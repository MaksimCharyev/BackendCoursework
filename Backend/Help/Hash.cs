namespace Backend.Help
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public static class PasswordHasher
    {
        // Метод для создания хэша пароля с использованием соли
        public static string CreateHashPassword(string login, string password)
        {
            // Генерация случайной соли
            byte[] saltBytes = GenerateSalt(login);

            // Конвертация пароля в байтовый массив
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            // Объединение пароля и соли
            byte[] saltedPassword = new byte[passwordBytes.Length + saltBytes.Length];
            Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
            Buffer.BlockCopy(saltBytes, 0, saltedPassword, passwordBytes.Length, saltBytes.Length);

            // Вычисление хэша пароля
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(saltedPassword);
                return Convert.ToBase64String(hashBytes);
            }
        }

        // Метод для проверки соответствия хэша пароля
        public static bool VerifyPassword(string login, string password, string hashPassword)
        {
            // Конвертация хэша пароля из строки в байтовый массив
            byte[] savedHashBytes = Convert.FromBase64String(hashPassword);

            // Получение соли, основываясь на логине пользователя
            byte[] saltBytes = GenerateSalt(login);

            // Конвертация пароля в байтовый массив
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            // Объединение пароля и соли
            byte[] saltedPassword = new byte[passwordBytes.Length + saltBytes.Length];
            Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
            Buffer.BlockCopy(saltBytes, 0, saltedPassword, passwordBytes.Length, saltBytes.Length);

            // Вычисление хэша пароля для сравнения с сохраненным хэшем
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputHashBytes = sha256.ComputeHash(saltedPassword);
                return CompareByteArrays(savedHashBytes, inputHashBytes);
            }
        }

        // Метод для генерации соли на основе логина пользователя
        private static byte[] GenerateSalt(string login)
        {
            byte[] saltBytes = Encoding.UTF8.GetBytes(login);
            return saltBytes;
        }

        // Метод для сравнения двух байтовых массивов
        private static bool CompareByteArrays(byte[] array1, byte[] array2)
        {
            if (array1 == null || array2 == null || array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
