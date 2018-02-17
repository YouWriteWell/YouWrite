// Copyright (c) 2014 Ravi Bhavnani
// License: Code Project Open License
// http://www.codeproject.com/info/cpol10.aspx

using System;
using System.Collections.Generic;
//using System.Link;
using System.Web;

namespace RavSoft.GoogleTranslator
{
    /// <summary>
    /// Translates text using Google's online language tools.
    /// </summary>
    public class Translator : RavSoft.WebResourceProvider
    {
        #region Constructor

            /// <summary>
            /// Initializes a new instance of the <see cref="Translator"/> class.
            /// </summary>
            public Translator()
            {
                this.SourceLanguage = "English";
                this.TargetLanguage = "French";
                this.Referer = "http://translate.google.com/";
            }

        #endregion

        #region Properties

            /// <summary>
            /// Gets or sets the source language.
            /// </summary>
            /// <value>The source language.</value>
            public string SourceLanguage {
                get;
                set;
            }

            /// <summary>
            /// Gets or sets the target language.
            /// </summary>
            /// <value>The target language.</value>
            public string TargetLanguage {
                get;
                set;
            }

            /// <summary>
            /// Gets or sets the source text.
            /// </summary>
            /// <value>The source text.</value>
            public string SourceText {
                get;
                set;
            }

            /// <summary>
            /// Gets the translation.
            /// </summary>
            /// <value>The translated text.</value>
            public string Translation {
                get;
                private set;
            }

            /// <summary>
            /// Gets the url used to speak the translation.
            /// </summary>
            /// <value>The url used to speak the translation.</value>
            public string TranslationSpeakUrl {
                get;
                private set;
            }

            /// <summary>
            /// Gets the time taken to perform the translation.
            /// </summary>
            /// <value>The time taken to perform the translation.</value>
            public TimeSpan TranslationTime {
                get;
                private set;
            }

            /// <summary>
            /// Gets the supported languages.
            /// </summary>
            public static IEnumerable<string> Languages {
                get {
                    Translator.EnsureInitialized();
                    // orignial:   return Translator._languageModeMap.Keys.OrderBy(p => p);
   
                    return Translator._languageModeMap.Keys;
                }
            }

        #endregion        

        #region Public methods

            /// <summary>
            /// Attempts to translate the text.
            /// </summary>
            public void Translate()
            {
                // Validate source and target languages
                if (string.IsNullOrEmpty (this.SourceLanguage) ||
                    string.IsNullOrEmpty (this.TargetLanguage) ||
                    this.SourceLanguage.Trim().Equals (this.TargetLanguage.Trim())) {
                    throw new Exception ("An invalid source or target language was specified.");
                }

                // Delegate to base class
                DateTime start = DateTime.Now;
                this.FetchResource();
                this.TranslationTime = DateTime.Now - start;
            }

        #endregion

        #region WebResourceProvider implementation

            /// <summary>
            /// Returns the url to be fetched.
            /// </summary>
            /// <returns>The url to be fetched.</returns> 
            protected override string GetFetchUrl()
            {
                string url = string.Format ("http://translate.google.com/translate_a/t?client=t&sl={0}&tl={1}&ie=UTF-8&oe=UTF-8&q={2}",
                                            Translator.LanguageEnumToIdentifier (this.SourceLanguage),
                                            Translator.LanguageEnumToIdentifier (this.TargetLanguage),
                                            HttpUtility.UrlEncode (this.SourceText));
                return url;
            }

            /// <summary>
            /// Parses the fetched content.
            /// </summary>
            protected override void ParseContent()
            {
                // Initialize the parser
                this.Translation = string.Empty;
                string strContent = this.Content;
                RavSoft.StringParser parser = new RavSoft.StringParser (strContent);

                // Extract the translation
                string strTranslation = string.Empty;
                if (parser.skipToEndOf ("[[[\"")) {
                    bool morePhrasesRemaining = true;
                    do {
                        string translatedPhrase = null;
                        if (parser.extractTo ("\",\"", ref translatedPhrase)) {
                            strTranslation += translatedPhrase;
                        }
                        morePhrasesRemaining = parser.skipToEndOf (",\"\",\"\"],[\"");
                    }
                    while (morePhrasesRemaining);
                }
                this.Translation = strTranslation.Replace (" .", ".").Replace (" ?", "?").Replace (" ,", ",").Replace (" ;", ";").Replace (" !", "!");

                // Set the translation speak url
                this.TranslationSpeakUrl = string.Format ("http://translate.google.com/translate_tts?ie=UTF-8&tl={0}&q={1}",
                                                          Translator.LanguageEnumToIdentifier (this.TargetLanguage),
                                                          HttpUtility.UrlEncode (this.Translation));
            }

        #endregion

        #region Private methods

            /// <summary>
            /// Converts a language to its identifier.
            /// </summary>
            /// <param name="language">The language."</param>
            /// <returns>The identifier or <see cref="string.Empty"/> if none.</returns>
            private static string LanguageEnumToIdentifier
                (string language)
            {
                string mode = string.Empty;
                Translator.EnsureInitialized();
                Translator._languageModeMap.TryGetValue (language, out mode);
                return mode;
            }

            /// <summary>
            /// Ensures the translator has been initialized.
            /// </summary>
            private static void EnsureInitialized()
            {
                if (Translator._languageModeMap == null) {
                    Translator._languageModeMap = new Dictionary<string,string>();
                    Translator._languageModeMap.Add ("Afrikaans",   "af");
                    Translator._languageModeMap.Add ("Albanian",    "sq");
                    Translator._languageModeMap.Add ("Arabic",      "ar");
                    Translator._languageModeMap.Add ("Armenian",    "hy");
                    Translator._languageModeMap.Add ("Azerbaijani", "az");
                    Translator._languageModeMap.Add ("Basque",      "eu");
                    Translator._languageModeMap.Add ("Belarusian",  "be");
                    Translator._languageModeMap.Add ("Bengali",     "bn");
                    Translator._languageModeMap.Add ("Bulgarian",   "bg");
                    Translator._languageModeMap.Add ("Catalan",     "ca");
                    Translator._languageModeMap.Add ("Chinese",     "zh-CN");
                    Translator._languageModeMap.Add ("Croatian",    "hr");
                    Translator._languageModeMap.Add ("Czech",       "cs");
                    Translator._languageModeMap.Add ("Danish",      "da");
                    Translator._languageModeMap.Add ("Dutch",       "nl");
                    Translator._languageModeMap.Add ("English",     "en");
                    Translator._languageModeMap.Add ("Esperanto",   "eo");
                    Translator._languageModeMap.Add ("Estonian",    "et");
                    Translator._languageModeMap.Add ("Filipino",    "tl");
                    Translator._languageModeMap.Add ("Finnish",     "fi");
                    Translator._languageModeMap.Add ("French",      "fr");
                    Translator._languageModeMap.Add ("Galician",    "gl");
                    Translator._languageModeMap.Add ("German",      "de");
                    Translator._languageModeMap.Add ("Georgian",    "ka");
                    Translator._languageModeMap.Add ("Greek",       "el");
                    Translator._languageModeMap.Add ("Haitian Creole",    "ht");
                    Translator._languageModeMap.Add ("Hebrew",      "iw");
                    Translator._languageModeMap.Add ("Hindi",       "hi");
                    Translator._languageModeMap.Add ("Hungarian",   "hu");
                    Translator._languageModeMap.Add ("Icelandic",   "is");
                    Translator._languageModeMap.Add ("Indonesian",  "id");
                    Translator._languageModeMap.Add ("Irish",       "ga");
                    Translator._languageModeMap.Add ("Italian",     "it");
                    Translator._languageModeMap.Add ("Japanese",    "ja");
                    Translator._languageModeMap.Add ("Korean",      "ko");
                    Translator._languageModeMap.Add ("Lao",         "lo");
                    Translator._languageModeMap.Add ("Latin",       "la");
                    Translator._languageModeMap.Add ("Latvian",     "lv");
                    Translator._languageModeMap.Add ("Lithuanian",  "lt");
                    Translator._languageModeMap.Add ("Macedonian",  "mk");
                    Translator._languageModeMap.Add ("Malay",       "ms");
                    Translator._languageModeMap.Add ("Maltese",     "mt");
                    Translator._languageModeMap.Add ("Norwegian",   "no");
                    Translator._languageModeMap.Add ("Persian",     "fa");
                    Translator._languageModeMap.Add ("Polish",      "pl");
                    Translator._languageModeMap.Add ("Portuguese",  "pt");
                    Translator._languageModeMap.Add ("Romanian",    "ro");
                    Translator._languageModeMap.Add ("Russian",     "ru");
                    Translator._languageModeMap.Add ("Serbian",     "sr");
                    Translator._languageModeMap.Add ("Slovak",      "sk");
                    Translator._languageModeMap.Add ("Slovenian",   "sl");
                    Translator._languageModeMap.Add ("Spanish",     "es");
                    Translator._languageModeMap.Add ("Swahili",     "sw");
                    Translator._languageModeMap.Add ("Swedish",     "sv");
                    Translator._languageModeMap.Add ("Tamil",       "ta");
                    Translator._languageModeMap.Add ("Telugu",      "te");
                    Translator._languageModeMap.Add ("Thai",        "th");
                    Translator._languageModeMap.Add ("Turkish",     "tr");
                    Translator._languageModeMap.Add ("Ukrainian",   "uk");
                    Translator._languageModeMap.Add ("Urdu",         "ur");
                    Translator._languageModeMap.Add ("Vietnamese",  "vi");
                    Translator._languageModeMap.Add ("Welsh",       "cy");
                    Translator._languageModeMap.Add ("Yiddish",     "yi");
                }
            }

        #endregion

        #region Fields

            /// <summary>
            /// The language to translation mode map.
            /// </summary>
            private static Dictionary<string, string> _languageModeMap;

        #endregion
    }
}
