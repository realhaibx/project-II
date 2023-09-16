using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using StackMaker.Code.Script.Value;
using UnityEngine;

namespace StackMaker.Code.Script.Service.DataService
{
    public class JsonDataService : IDataService
    {
        #region VARIABLES

        #region PRIVATE

        private static readonly string KEY = Values.DataService.DEFAULT_KEY;
        private static readonly string IV = Values.DataService.DEFAULT_IV;

        #endregion

        #endregion

        #region FUNCTIONS

        #region USER DEFINED PRIVATE

        /// <summary>
        /// Delete file if it existed
        /// </summary>
        /// <param name="path">File path</param>
        private static void DeleteIfExist(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        /// <summary>
        /// Throw FileNotFoundException if path is not exist
        /// </summary>
        /// <param name="path">Path directory</param>
        /// <returns>Is throw exception away</returns>
        /// <exception cref="FileNotFoundException">File not exist</exception>
        private bool ThrowIfNotExists(string path)
        {
            bool isThrowAway = !File.Exists(path);

            return isThrowAway;
        }

        /// <summary>
        /// Write encrypted data to file stream
        /// </summary>
        /// <param name="data">Data to write</param>
        /// <param name="stream">Stream file</param>
        private void WriteEncryptedData<T>(T data, FileStream stream)
        {
            using Aes aesProvider = Aes.Create();

            aesProvider.Key = Convert.FromBase64String(KEY);
            aesProvider.IV = Convert.FromBase64String(IV);

            using ICryptoTransform cryptoTransform = aesProvider.CreateEncryptor();

            using CryptoStream cryptoStream = new CryptoStream(
                stream,
                cryptoTransform,
                CryptoStreamMode.Write
            );

            string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            cryptoStream.Write(Encoding.ASCII.GetBytes(jsonData), 0, Encoding.ASCII.GetByteCount(jsonData));
        }

        /// <summary>
        /// Read data from encrypted file
        /// </summary>
        /// <param name="path">Data path</param>
        /// <typeparam name="T">Class type</typeparam>
        /// <returns>Decrypted data</returns>
        private T ReadEncryptedData<T>(string path)
        {
            // Read path from file
            byte[] fileBytes = File.ReadAllBytes(path);
            
            // Decrypt data from byte
            using Aes aesProvider = Aes.Create();

            aesProvider.Key = Convert.FromBase64String(KEY);
            aesProvider.IV = Convert.FromBase64String(IV);

            using ICryptoTransform cryptoTransform = aesProvider.CreateDecryptor(
                aesProvider.Key,
                aesProvider.IV
            );
            
            using MemoryStream decryptionStream = new MemoryStream(fileBytes);
            
            using CryptoStream cryptoStream = new CryptoStream(
                decryptionStream,
                cryptoTransform,
                CryptoStreamMode.Read
            );
            
            using StreamReader reader = new StreamReader(cryptoStream);

            string result = reader.ReadToEnd();

            return JsonConvert.DeserializeObject<T>(result);
        }

        #endregion

        #region USER DEFINED PUBLIC

        /// <summary>
        /// Save data to file with custom class and option to encrypt data
        /// </summary>
        /// <param name="relativePath">Data file directory</param>
        /// <param name="data">Class to save data</param>
        /// <param name="isEncrypt">Encrypt data</param>
        /// <typeparam name="T">Class type</typeparam>
        /// <returns>Save progress completion</returns>
        public bool SaveData<T>(string relativePath, T data, bool isEncrypt)
        {
            var dataPath = AbsolutePathOf(relativePath);

            try
            {
                DeleteIfExist(dataPath);

                using FileStream stream = File.Create(dataPath);

                // Write data 
                if (isEncrypt)
                {
                    WriteEncryptedData(data, stream); // encrypted
                }
                else
                {
                    stream.Close();
                    File.WriteAllText(dataPath, JsonConvert.SerializeObject(data, Formatting.Indented,
                        new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })); // raw
                }

                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Can't save data due to {e.Message} {e.StackTrace}");
                return false;
            }
        }

        /// <summary>
        /// Load data from path
        /// </summary>
        /// <param name="relativePath">Data file directory</param>
        /// <param name="isEncrypt">Encrypt data</param>
        /// <typeparam name="T">Class type</typeparam>
        /// <returns>Loaded data</returns>
        public T LoadData<T>(string relativePath, bool isEncrypt)
        {
            string dataPath = AbsolutePathOf(relativePath);

            // Load data from path or return default data class
            if (ThrowIfNotExists(dataPath))
            {
                return default(T);
            }
            else
            {
                try
                {
                    var data = isEncrypt ? ReadEncryptedData<T>(dataPath) : JsonConvert.DeserializeObject<T>(File.ReadAllText(dataPath));

                    return data;
                }
                catch (Exception e)
                {
                    Debug.LogError($"Failed to load data due to: {e.Message} {e.StackTrace}");
                    throw;
                }
            }
        }
        
        /// <summary>
        /// Get the absolute path from relative path in runtime
        /// </summary>
        /// <param name="path"> Relative path</param>
        /// <returns>Absolute path in runtime</returns>
        public string AbsolutePathOf(string path)
        {
            var parentDirectoryPath = Application.persistentDataPath; // Parent path on build

            #if !UNITY_EDITOR
            parentDirectoryPath = Application.persistentDataPath;            
            #endif
            
            #if UNITY_EDITOR
            parentDirectoryPath = Application.dataPath; // Parent path on editor
            #endif

            return $"{parentDirectoryPath}{path}";
        }

        #endregion

        #endregion
    }
}