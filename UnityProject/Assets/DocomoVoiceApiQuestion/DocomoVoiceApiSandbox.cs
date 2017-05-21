using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

namespace DocomoVoiceApiQuestion
{
    // resolve for https://teratail.com/questions/76058

    public class DocomoVoiceApiSandbox : MonoBehaviour
    {
        static readonly string docomoApiKey = @"<your api key>";
        static readonly string SaveSoundFileName = @"docomoApiSound.wav";

        public AudioClipMaker audioClipMaker;
        public AudioSource audioSource;

        public void OnButtonClick()
        {
            StartCoroutine(DownloadCoroutine());
        }

        public void OnPlayButtonClick()
        {
            PlaySound();
        }

        IEnumerator DownloadCoroutine()
        {
            string url = "https://api.apigw.smt.docomo.ne.jp/aiTalk/v1/textToSpeech?APIKEY=" + docomoApiKey;

            Dictionary<string, string> aiTalksParams = new Dictionary<string, string>();
            aiTalksParams["pitch"] = "1";
            aiTalksParams["range"] = "1";
            aiTalksParams["rate"] = "1";
            aiTalksParams["volume"] = "1.5";

            var ssml = CreateSSML(aiTalksParams);
            byte[] postData = System.Text.Encoding.UTF8.GetBytes(ssml);

            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers["Content-Type"] = "application/ssml+xml";
            headers["Accept"] = "audio/L16";
            WWW www = new WWW(url, postData, headers);

            yield return www;
            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.LogError(www.error);
                yield break;
            }

            uint fs = 16000; // 16K

            WAVHDR wavHdr = new WAVHDR()
            {
                formatid = 0x0001, //PCM 非圧縮
                channel = 1, // ch=1 モノラル
                fs = fs,    //
                bytespersec = fs * 2, // 16bit 16K
                blocksize = 2, // ブロックサイズ (Byte/sample×チャンネル数)->→16ビットモノラルなので 2
                bitspersample = 16, // サンプルあたりのビット数 (bit/sample)
                size = 10 * fs * 2, // 波形データのバイト数
            };
            wavHdr.fileSize = wavHdr.size + (uint)Marshal.SizeOf(wavHdr); // 全体のバイト数

            Debug.Log("SaveWaveFile");
            SaveWaveFile(wavHdr, ConvertBytesEndian(www.bytes));
        }


        private void SaveWaveFile(WAVHDR hdr, byte[] databuf)
        {
            using (FileStream fs = new FileStream(SaveSoundFileName, FileMode.Create, FileAccess.Write))
            using (BinaryWriter bWriter = new BinaryWriter(fs))
            {
                // ヘッダ書きだし
                foreach (byte b in hdr.getByteArray())
                {
                    bWriter.Write(b);
                }
                // 波形書きだし
                foreach (var data in databuf)
                {
                    bWriter.Write(data);
                }
                bWriter.Flush();
            }
        }

        byte[] ConvertBytesEndian(byte[] bytes)
        {
            byte[] newBytes = new byte[bytes.Length];
            for (int i = 0; i < bytes.Length; i += 2)
            {
                newBytes[i] = bytes[i + 1];
                newBytes[i + 1] = bytes[i];
            }
            return newBytes;
        }

        protected string CreateSSML(Dictionary<string, string> parts)
        {
            return @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<speak version = ""1.1"">
<voice name = ""nozomi"">
のぞみです。おはようございます。
</voice >
<break time = ""1000ms"" />
<voice name = ""seiji"">
 せいじです。こんにちは。
</voice>
</speak>";
        }

        private void PlaySound()
        {
            byte[] buf = File.ReadAllBytes(SaveSoundFileName);

            var wavFileInfo = new WavFileInfo();
            wavFileInfo.Analyze(buf);

            AudioSource source = audioSource;
            source.clip = audioClipMaker.Create(
                "making_clip",
                buf,
                wavFileInfo.TrueWavBufIdx,
                wavFileInfo.BitPerSample,
                wavFileInfo.TrueSamples,
                wavFileInfo.Channels,
                wavFileInfo.Frequency,
                false
            );
            source.Play();
        }
    }
}