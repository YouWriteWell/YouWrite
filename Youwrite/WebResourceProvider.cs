// Copyright (c) 2014 Ravi Bhavnani
// License: Code Project Open License
// http://www.codeproject.com/info/cpol10.aspx

using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text;
using System.Threading;

namespace RavSoft
{
	/// <summary>
	/// A framework to expose information rendered by a URL (i.e. a "web
	/// resource") as an object that can be manipulated by an application.
	/// You use WebResourceProvider by deriving from it and implementing
	/// getFetchUrl() and optionally overriding other methods.
	/// </summary>
	abstract public class WebResourceProvider
	{
        #region Constructor

            /// <summary>
            /// Initializes a new instance of the <see cref="WebResourceProvider"/> class.
            /// </summary>
		    public WebResourceProvider()
		    {
		        Reset();
		    }

        #endregion

        #region Properties

            /// <summary>
            /// Gets and sets the user agent string.
            /// </summary>
            public string Agent {
              get { return m_strAgent; }
              set { m_strAgent = (value == null ? "" : value); }
            }

            /// <summary>
            /// Gets or sets the cache level.
            /// </summary>
            public RequestCacheLevel CacheLevel {
                get;
                set;
            }

            /// <summary>
            /// Gets and sets the referer string.
            /// </summary>
            public string Referer {
              get { return m_strReferer; }
              set { m_strReferer = (value == null ? "" : value); }
            }

            /// <summary>
            /// Gets and sets the minimum pause time interval (in mSec).
            /// </summary>
            public int Pause {
              get { return m_nPause; }
              set { m_nPause = value; }
            }

            /// <summary>
            /// Gets and sets the timeout (in mSec).
            /// </summary>
            public int Timeout {
              get { return m_nTimeout; }
              set { m_nTimeout = value; }
            }

            /// <summary>
            /// Returns the retrieved content.
            /// </summary>
            public string Content {
              get { return m_strContent; }
            }

            /// <summary>
            /// Gets the fetch timestamp.
            /// </summary>
            public DateTime FetchTime {
              get { return m_tmFetchTime; }
            }

            /// <summary>
            /// Gets the last error message, if any.
            /// </summary>
            public string ErrorMsg {
              get { return m_strError; }
            }

        #endregion

        #region Public methods

            /// <summary>
            /// Resets the state of the object.
            /// </summary>
            public void Reset()
            {
                m_strAgent = "Mozilla/5.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; SLCC1; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET CLR 1.1.4322)";
                this.CacheLevel = RequestCacheLevel.NoCacheNoStore;
                m_strReferer = string.Empty;
                m_strError = string.Empty;
                m_strContent = string.Empty;
                m_httpStatusCode = HttpStatusCode.OK;
                m_nPause = 0;
                m_nTimeout = 0;
                m_tmFetchTime = DateTime.MinValue;
            }

            /// <summary>
            /// Fetches the web resource.
            /// </summary>
            public void FetchResource()
            {
                // Initialize the provider - quit if initialization fails
                if (!this.Init())
                    return;

                // Main loop
                this.m_tmFetchTime = DateTime.MinValue;
                bool bOK = false;
                do {
                    string url = this.GetFetchUrl();
                    this.GetContent (url);
                    bOK = (m_httpStatusCode == HttpStatusCode.OK);
                    if (bOK)
                        this.ParseContent();
                }
                while (bOK && this.ContinueFetching());
            }

        #endregion

        #region Virtual methods

            /// <summary>
            /// Provides the derived class with an opportunity to initialize itself.
            /// </summary>
            /// <returns>true if the operation succeeded, false otherwise.</returns>
            protected virtual bool Init()
            {
                return true;
            }

            /// <summary>
            /// Returns the url to be fetched.
            /// </summary>
            /// <returns>The url to be fetched.</returns>
            abstract protected string GetFetchUrl();

            /// <summary>
            /// Retrieves the POST data (if any) to be sent to the url to be fetched.
            /// The data is returned as a string of the form &quot;arg=val [&amp;arg=val]...&quot;.
            /// </summary>
            /// <returns>A string containing the POST data or null if none.</returns>
            protected virtual string GetPostData()
            {
                return null;
            }

            /// <summary>
            /// Provides the derived class with an opportunity to parse the fetched content.
            /// </summary>
            protected virtual void ParseContent()
            {
            }

            /// <summary>
            /// Informs the framework that it needs to continue fetching urls.
            /// </summary>
            /// <returns>
            /// true if the framework needs to continue fetching urls, false otherwise.
            /// </returns>
            protected virtual bool ContinueFetching()
            {
                return false;
            }

        #endregion

        #region Private methods

            /// <summary>
            /// Retrieves the content of the url to be fetched.
            /// </summary>
            /// <param name="url">Url to be fetched.</param>
            private void GetContent
                (string url)
            {
                // Pause, if necessary
                if ((m_nPause > 0) && (m_tmFetchTime != DateTime.MinValue)) {
                    while ((DateTime.Now - m_tmFetchTime).TotalMilliseconds < m_nPause) {
                        Thread.Sleep (1);
                    }
                }

                // Set up the fetch request
                string strUrl = url;
                if (!strUrl.StartsWith ("http://"))
                    strUrl = "http://" + strUrl;
                HttpWebRequest req = (HttpWebRequest) WebRequest.Create (strUrl);
                req.CachePolicy = new RequestCachePolicy (this.CacheLevel);
                req.AllowAutoRedirect = true;
                req.UserAgent = m_strAgent;
                req.Referer = m_strReferer;
                if (m_nTimeout != 0)
                    req.Timeout = m_nTimeout;

                // Add POST data (if present)
                string strPostData = this.GetPostData();
                if (strPostData != null) {

                    ASCIIEncoding asciiEncoding = new ASCIIEncoding();
                    byte[] postData = asciiEncoding.GetBytes (strPostData);
                    req.Method = "POST";
                    req.ContentType="application/x-www-form-urlencoded";
                    req.ContentLength = postData.Length;

                    Stream reqStream = req.GetRequestStream();
                    reqStream.Write (postData, 0, postData.Length);
                    reqStream.Close();
                }

                // Fetch the url - return on error
                m_strError = "";
                m_strContent = "";
                HttpWebResponse resp = null;
                try {
                    m_tmFetchTime = DateTime.Now;
                    resp = (HttpWebResponse) req.GetResponse();
                }
                catch (Exception exc) {
                    if (exc is WebException) {
                        WebException webExc = exc as WebException;
                        m_strError = webExc.Message;
                    }
                    return;
                }
                finally {
                    if (resp != null)
                        m_httpStatusCode = resp.StatusCode;
                }

                // Store retrieved content
                try {
                    Stream stream = resp.GetResponseStream();
                    StreamReader streamReader = new StreamReader (stream);
                    m_strContent = streamReader.ReadToEnd();
                }
                catch (Exception) {
                    // Read failure occured - nothing to do
                }
            }

        #endregion

        #region Fields

            /// <summary>
            /// User agent string used when making an HTTP request.
            /// </summary>
            private string m_strAgent;

            /// <summary>
            /// Referer string used when making an HTTP request.
            /// </summary>
            private string m_strReferer;

            /// <summary>
            /// Error message.
            /// </summary>
            string m_strError;

            /// <summary>
            /// Retrieved content.
            /// </summary>
            string m_strContent;

            /// <summary>
            /// HTTP status code.
            /// </summary>
            HttpStatusCode m_httpStatusCode;

            /// <summary>
            /// Minimum number of mSecs to pause between successive HTTP requests.
            /// </summary>
            int m_nPause;

            /// <summary>
            /// HTTP request timeout (in mSecs).
            /// </summary>
            int m_nTimeout;

            /// <summary>
            /// Timestamp of last fetch.
            /// </summary>
            DateTime m_tmFetchTime;

        #endregion
	}
}