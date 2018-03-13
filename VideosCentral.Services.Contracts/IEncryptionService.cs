namespace VideosCentral.Services.Contracts
{
    /// <summary>
    /// Set of methods used to encrypt/decrypt a string. 
    /// </summary>
    public interface IEncryptionService
    {
        /// <summary>
        /// Method encrypting string passed as parameter
        /// </summary>
        /// <param name="plainText">The string to encrypt</param>
        /// <param name="passPhrase">Crypting key</param>
        /// <returns>Encrypted string</returns>
        string Encrypt(string plainText, string passPhrase);

        /// <summary>
        /// Method decrypting string passed as parameter
        /// </summary>
        /// <param name="encryptedText">The string to decrypt</param>
        /// <param name="passPhrase">Decrypting key</param>
        /// <returns>Decrypted string</returns>
        string Decrypt(string encryptedText, string passPhrase);
    }
}