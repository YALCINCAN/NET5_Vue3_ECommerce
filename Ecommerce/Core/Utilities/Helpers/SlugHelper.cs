using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Utilities
{
    public static class SlugHelper
    {
        public static string Slugify(string IncomingText)
        {
            IncomingText = IncomingText.Replace("ş", "s");
            IncomingText = IncomingText.Replace("Ş", "s");
            IncomingText = IncomingText.Replace("İ", "i");
            IncomingText = IncomingText.Replace("I", "i");
            IncomingText = IncomingText.Replace("ı", "i");
            IncomingText = IncomingText.Replace("ö", "o");
            IncomingText = IncomingText.Replace("Ö", "o");
            IncomingText = IncomingText.Replace("ü", "u");
            IncomingText = IncomingText.Replace("Ü", "u");
            IncomingText = IncomingText.Replace("Ç", "c");
            IncomingText = IncomingText.Replace("ç", "c");
            IncomingText = IncomingText.Replace("ğ", "g");
            IncomingText = IncomingText.Replace("Ğ", "g");
            IncomingText = IncomingText.Replace(" ", "-");
            IncomingText = IncomingText.Replace("---", "-");
            IncomingText = IncomingText.Replace("?", "");
            IncomingText = IncomingText.Replace("/", "");
            IncomingText = IncomingText.Replace(".", "");
            IncomingText = IncomingText.Replace("'", "");
            IncomingText = IncomingText.Replace("#", "sharp");
            IncomingText = IncomingText.Replace("%", "");
            IncomingText = IncomingText.Replace("&", "");
            IncomingText = IncomingText.Replace("*", "");
            IncomingText = IncomingText.Replace("!", "");
            IncomingText = IncomingText.Replace("@", "");
            IncomingText = IncomingText.Replace("+", "");

            IncomingText = IncomingText.ToLower();
            IncomingText = IncomingText.Trim();

            // tüm harfleri küçült
            string encodedUrl = (IncomingText ?? "").ToLower();

            // & ile " " yer değiştirme
            encodedUrl = Regex.Replace(encodedUrl, @"&+", "and");

            // " " karakterlerini silme
            encodedUrl = encodedUrl.Replace("'", "");

            // geçersiz karakterleri sil
            encodedUrl = Regex.Replace(encodedUrl, @"[^a-z0-9]", "-");

            // tekrar edenleri sil
            encodedUrl = Regex.Replace(encodedUrl, @"-+", "-");

            // karakterlerin arasına tire koy
            encodedUrl = encodedUrl.Trim('-');

            return encodedUrl;
        }
    } 
}
