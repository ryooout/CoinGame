using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

class LocalData
{
    /// <summary>
    /// �t�@�C����ǂݍ��݂܂�
    /// </summary>
    /// <typeparam name="T">�N���X�̌^</typeparam>
    /// <param name="filepath">�t�@�C����</param>
    /// <param name="enc">�Í��������������Ƃ��Ɏw��</param>
    /// <returns></returns>
    static public T Load<T>(string filepath, bool enc = false)
    {
        //�t�@�C�����Ȃ�������default�ŕԂ�
        if (!File.Exists(filepath))
        {
            return default;
        }

        var data = File.ReadAllBytes(filepath);
#if RELEASE
        arr = AesDecrypt(arr);
#else
        if (enc)
        {
            data = AesDecrypt(data);
        }
#endif

        string json = Encoding.UTF8.GetString(data);
        return JsonUtility.FromJson<T>(json);
    }

    /// <summary>
    /// �t�@�C����ۑ����܂�
    /// </summary>
    /// <typeparam name="T">�N���X�̌^</typeparam>
    /// <param name="filepath">�t�@�C����</param>
    /// <param name="data">�Z�[�u����f�[�^</param>
    /// <param name="enc">�Í��������������Ƃ��Ɏw��</param>
    /// <returns></returns>
    static public void Save<T>(string filepath, T dataObj, bool enc = false)
    {
        var json = JsonUtility.ToJson(dataObj);
        byte[] data = Encoding.UTF8.GetBytes(json);
#if RELEASE
        arr = AesEncrypt(arr);
#else
        if (enc)
        {
            data = AesEncrypt(data);
        }
#endif
        var pathes = (filepath).Split('/').ToList();
        pathes.RemoveAt(pathes.Count - 1);
        var dir = string.Join("/", pathes);
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        File.WriteAllBytes(filepath, data);
    }

    /// <summary>
    /// �t�@�C���������܂�
    /// </summary>
    /// <param name="filepath">�t�@�C�������w��</param>
    /// <returns></returns>
    static public void Delete(string filepath)
    {
        if (!File.Exists(filepath))
        {
            return;
        }

        File.Delete(filepath);
    }


    //�������牺�͌��Ȃ��Ă����R�[�h


    /// <summary>
    /// AES�Í���
    /// </summary>
    static public byte[] AesEncrypt(byte[] byteText)
    {
        // AES�ݒ�l
        //===================================
        int aesKeySize = 128;
        int aesBlockSize = 128;
        string aesIv = "cCYcP6Is7Y6EpGN9";
        string aesKey = "ieSeuIx0ZF4s8s1M";
        //===================================

        // AES�}�l�[�W���[�擾
        var aes = GetAesManager(aesKeySize, aesBlockSize, aesIv, aesKey);
        // �Í���
        byte[] encryptText = aes.CreateEncryptor().TransformFinalBlock(byteText, 0, byteText.Length);

        return encryptText;
    }

    /// <summary>
    /// AES������
    /// </summary>
    static public byte[] AesDecrypt(byte[] byteText)
    {
        // AES�ݒ�l
        //===================================
        int aesKeySize = 128;
        int aesBlockSize = 128;
        string aesIv = "cCYcP6Is7Y6EpGN9";
        string aesKey = "ieSeuIx0ZF4s8s1M";
        //===================================

        // AES�}�l�[�W���[�擾
        var aes = GetAesManager(aesKeySize, aesBlockSize, aesIv, aesKey);
        // ������
        byte[] decryptText = aes.CreateDecryptor().TransformFinalBlock(byteText, 0, byteText.Length);

        return decryptText;
    }

    /// <summary>
    /// AesManaged���擾
    /// </summary>
    /// <param name="keySize">�Í������̒���</param>
    /// <param name="blockSize">�u���b�N�T�C�Y</param>
    /// <param name="iv">�������x�N�g��(���pX�����i8bit * X = [keySize]bit))</param>
    /// <param name="key">�Í����� (��X�����i8bit * X���� = [keySize]bit�j)</param>
    static private AesManaged GetAesManager(int keySize, int blockSize, string iv, string key)
    {
        AesManaged aes = new AesManaged();
        aes.KeySize = keySize;
        aes.BlockSize = blockSize;
        aes.Mode = CipherMode.CBC;
        aes.IV = Encoding.UTF8.GetBytes(iv);
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.Padding = PaddingMode.PKCS7;
        return aes;
    }


    /// <summary>
    /// XOR
    /// </summary>
    static public byte[] Xor(byte[] a, byte[] b)
    {
        int j = 0;
        for (int i = 0; i < a.Length; i++)
        {
            if (j < b.Length)
            {
                j++;
            }
            else
            {
                j = 1;
            }
            a[i] = (byte)(a[i] ^ b[j - 1]);
        }
        return a;
    }
}