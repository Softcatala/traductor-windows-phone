/*
 * Copyright (C) 2014 Bernat Mut <berni.emerald@gmail.com>
 * 
 * This file is part of Traductor Softcatala.
 * Traductor Softcatala is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License as
 * published by the Free Software Foundation; either version 2 of the
 * License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * General Public License for more details.
 *
 * You should have received a copy of the GNU General Public
 * License along with this program; if not, write to the
 * Free Software Foundation, Inc., 59 Temple Place - Suite 330,
 * Boston, MA 02111-1307, USA.
 */
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace TraductorSoftcatala
{
    public class JSonHelper
    {
            private static  String SERVER_URL = "http://www.softcatala.org/apertium/json/translate";
            private static String KEY = "ZjYxNGViMzkxMTg2MzNlZmY3Yjk";
            String _language;

            public delegate void RecievedTranslationHandler(object sender, TranslationEventArgs e);

        public event RecievedTranslationHandler TranslationEvent;

        protected void OnRecievedTranslation(TranslationEventArgs e)
        {
            TranslationEvent(this, e);
        }  

        public String getName() {
            return _language;
        }

        public void setName(String name) {
            _language = name;
        }

        // Adds a query parameter to an URL 	
        String AddQueryParameter(String key, String value) {
            StringBuilder sb = new StringBuilder();

            sb.Append("&");
            sb.Append(key);
            sb.Append("=");
            try {
                sb.Append(Uri.EscapeDataString(value));
            } catch (Exception e) {
                Debug.WriteLine( e.StackTrace);
            }

            return sb.ToString();
        }

        private Uri BuildURL(String langCode, String text) {
            StringBuilder sb = new StringBuilder();
            sb.Append(SERVER_URL);
            sb.Append("?");
            sb.Append(AddQueryParameter("markUnknown", "yes"));
            sb.Append(AddQueryParameter("key", KEY));
            sb.Append(AddQueryParameter("langpair", langCode));
            sb.Append(AddQueryParameter("q", text));

            return new Uri(sb.ToString());
        }



        public string Translated { get;private set; }


        public void sendJson(String langCode, String text) {


                WebClient client = new WebClient();

                Uri url = BuildURL(langCode, text);
                client.DownloadStringCompleted += StringDownloadCompleted;
                client.DownloadStringAsync(url);

          

 

            
        }

        private void StringDownloadCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
          
                if (e.Error != null)
                {
                    return;
                }

                if (!string.IsNullOrEmpty(e.Result))
                {
                    string json = e.Result;

                   // JArray x = new JArray(e.Result);
                    //Translated = x["responseData"]["translatedText"].ToString();

                    TranslateResponse res = JsonConvert.DeserializeObject<TranslateResponse>(json);
                    Translated = res.responseData.translatedText;
                    TranslationEventArgs args = new TranslationEventArgs(res);
                    OnRecievedTranslation(args);

                  
                }


                ((WebClient)sender).DownloadStringCompleted -= StringDownloadCompleted;
                

           
        }



        private static String toString(Stream inputStream) {
            StringBuilder outputBuilder = new StringBuilder();
            try {
                String str;
                if (inputStream != null) {
                    StreamReader reader =
                            new StreamReader(inputStream, Encoding.UTF8);
                    while (null != (str = reader.ReadLine())) {
                        outputBuilder.Append(str).Append('\n');
                    }
                }
            } catch (Exception ex) {
                Debug.WriteLine("Error reading translation stream.", ex);
            }
            return outputBuilder.ToString();
        }
        public class TranslationEventArgs : EventArgs
        {
            private TranslateResponse _response;
               public TranslationEventArgs(TranslateResponse Message)
               {
                   this._response = Message;
               }
               public string Message { get { return this._response.responseData.translatedText; } } 
               public TranslateResponse Response
               {
                   get { return this._response; }
                   set { this._response = value; }
               }   


        }

    }

    public class TranslateResponse{
    
        public ResponseData responseData;
        public string responseDetails;
        public int responseStatus;
    
    }
    public class ResponseData{
        public string translatedText;

    }

}
