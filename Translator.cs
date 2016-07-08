using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Windows;
using System.Xml.Linq;

namespace WPF_Language_Translator
{
    class Translator
    {

        private string appID = "6CE9C85A41571C050C379F60DA173D286384E0F2";

        private List<Language> liste = new List<Language>();

        public Translator()
        {
            this.PopulateLanguageList();
        }

       
        public List<Language> DillerListesi
        {
            get
            {
                return (liste.OrderBy(l => l.dil).ToList());
            }
        }

        private void PopulateLanguageList()
        {
            liste.Add(new Language {dil = "Arabic", dil_kod = "ar"});
            liste.Add(new Language {dil = "Czech", dil_kod = "cs"});
            liste.Add(new Language {dil = "Danish", dil_kod = "da"});
            liste.Add(new Language {dil = "German", dil_kod = "de"});
            liste.Add(new Language {dil = "English", dil_kod = "en"});
            liste.Add(new Language {dil = "Estonian", dil_kod = "et"});
            liste.Add(new Language {dil = "Finnish", dil_kod = "fi"});
            liste.Add(new Language {dil = "Dutch", dil_kod = "nl"});
            liste.Add(new Language {dil = "Greek", dil_kod = "el"});
            liste.Add(new Language {dil = "Hebrew", dil_kod = "he"});
            liste.Add(new Language {dil = "Haitian Creole", dil_kod = "ht"});
            liste.Add(new Language {dil = "Hindi", dil_kod = "hi"});
            liste.Add(new Language {dil = "Hungarian", dil_kod = "hu"});
            liste.Add(new Language {dil = "Indonesian", dil_kod = "id"});
            liste.Add(new Language {dil = "Italian", dil_kod = "it"});
            liste.Add(new Language {dil = "Japanese", dil_kod = "ja"});
            liste.Add(new Language {dil = "Korean", dil_kod = "ko"});
            liste.Add(new Language {dil = "Lithuanian", dil_kod = "lt"});
            liste.Add(new Language {dil = "Latvian", dil_kod = "lv"});
            liste.Add(new Language {dil = "Norwegian", dil_kod = "no"});
            liste.Add(new Language {dil = "Polish", dil_kod = "pl"});
            liste.Add(new Language {dil = "Portuguese", dil_kod = "pt"});
            liste.Add(new Language {dil = "Romanian", dil_kod = "ro"});
            liste.Add(new Language {dil = "Spanish", dil_kod = "es"});
            liste.Add(new Language {dil = "Russian", dil_kod = "ru"});
            liste.Add(new Language {dil = "Slovak", dil_kod = "sk"});
            liste.Add(new Language {dil = "Slovene", dil_kod = "sl"});
            liste.Add(new Language {dil = "Swedish", dil_kod = "sv"});
            liste.Add(new Language {dil = "Thai", dil_kod = "th"});
            liste.Add(new Language {dil = "Turkish", dil_kod = "tr"});
            liste.Add(new Language {dil = "Ukranian", dil_kod = "uk"});
            liste.Add(new Language {dil = "Vietnamese", dil_kod = "vi"});
            liste.Add(new Language {dil = "Simplified Chinese", dil_kod = "zh-CHS"});
            liste.Add(new Language {dil = "Traditional Chinese", dil_kod = "zh-CHT"}); 
        }

        
        public string GetTranslatedText(string textToTranslate, string fromLang, string toLang)
        {
            string ceviri;

            if (fromLang != toLang)
            {
                string uri = "http://api.microsofttranslator.com/v2/Http.svc/Translate?appId=" +
                            appID + "&text=" + textToTranslate + "&from=" + fromLang + "&to=" + toLang;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

                try
                {
                    WebResponse response = request.GetResponse();
                    Stream strm = response.GetResponseStream();
                    StreamReader sr = new StreamReader(strm);
                    ceviri = sr.ReadToEnd();

                    response.Close();
                    sr.Close();
                }
                catch (WebException)
                {
                    MessageBox.Show("İnternet bağlantısı yok.",
                                "Translator", MessageBoxButton.OK, MessageBoxImage.Stop);
                    return (string.Empty);
                }
            }
            else
            {
                MessageBox.Show("Benzer bir dile çevrilemedi", "Translator",
                            MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return (string.Empty);
            }

          
            return (XElement.Parse(ceviri).Value);
        }
    }
}
