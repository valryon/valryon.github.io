using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace System
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>Author: Olivier De Bel-Air</remarks>
    public static class StringEx
    {
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static string RemoveHtmlTags(this string content)
        {
            if (content != null)
            {
                content = Regex.Replace(content, "<.*?>", string.Empty);
            }

            return content;
        }

        /// <summary>
        /// Permet de traiter la réécriture des liens pour retirer les critères 
        /// </summary>
        /// <param name="path">Chemin d'accès </param>
        /// <returns>Retourne une chaine de caractère nettoyée</returns>
        public static string ProcessPath(this string path)
        {
            // suppression des caractères spéciaux 
            path = path.RemoveSpecificCharacter();

            // Mise en minuscule
            path = path.ToLower();

            // Retirer les - en doublon...
            if (path != "-")
            {
                path = RemoveConsecutiveDuplicateCharater(path, '-');
            }

            return path;
        }

        private static string RemoveConsecutiveDuplicateCharater(string inputString, char character)
        {
            StringBuilder sb = new StringBuilder();

            string[] parts = inputString.Split(new char[] { character }, StringSplitOptions.RemoveEmptyEntries);

            int size = parts.Length;
            for (int i = 0; i < size; i++)
            {
                if (i == size - 1)
                {
                    sb.AppendFormat("{0}", parts[i]);
                }
                else
                {
                    sb.AppendFormat("{0}-", parts[i]);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Permet de supprimer une liste de caractère pour qu'il ne soit pas présent au sein de l'URL
        /// 
        /// remarques:
        ///  - cette méthode fait appel à la méthode RemoveDiacritics() pour gérer les caractères accentuées
        /// </summary>
        /// <param name="value">Chaine de caractère à modifier</param>
        /// <returns>Retourne la chaine de caractère modifiée</returns>
        public static string RemoveSpecificCharacter(this string value)
        {
            string result = value;
            if (value != null)
            {
                // FixMe : il faut plutôt utiliser une expression régulière
                value = value.Replace("\r", "");
                value = value.Replace("\n", "");
                value = value.Replace(" ", "-");
                value = value.Replace("&", "-et-");
                value = value.Replace(":", string.Empty);
                value = value.Replace("#", "sharp");
                value = value.Replace("*", string.Empty);
                value = value.Replace('"', '-');
                value = value.Replace(".", "dot");
                value = value.Replace('(', '-');
                value = value.Replace(")", string.Empty);
                value = value.Replace("?", string.Empty);
                value = value.Replace("!", string.Empty);
                value = value.Replace('[', '-');
                value = value.Replace(']', '-');
                value = value.Replace('\'', '-');
                value = value.Replace('/', '-');
                value = value.Replace(',', '-');
                value = value.Replace('+', 'p');
                value = value.Replace('*', '-');
                value = value.Replace("’", "-");
                value = value.Replace("%", "pour-cent");
                value = value.Replace("É", "e"); // vérifier l'utilité de ce remplacement
                value = value.Replace("<", "");
                value = value.Replace(">", "");
                value = value.Replace("ê", "e");

                result = value.RemoveDiacritics();
            }

            return result;
        }

        /// <summary>
        /// Cette méthode élimine les diacritiques (accents, cédilles, etc) d'une chaine de caractères, en les remplaçant par les caractères "de base"
        /// 
        /// exemple:
        ///     ça c'êst bïén => ca c'est bien
        /// </summary>
        /// <param name="s">une chaîne de caractères à modifier</param>
        /// <returns>la chaîne de caractères modifiée</returns>
        public static string RemoveDiacritics(this string s)
        {
            string normalizedString = s.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                char c = normalizedString[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString();
        }
    }
}