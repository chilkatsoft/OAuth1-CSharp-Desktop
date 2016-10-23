using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OAuth1
    {
    public partial class Form1 : Form
        {
        public Form1()
            {
            InitializeComponent();
            }

        // -----------------------------
        // Twitter Application Settings
        // -----------------------------
        //Access level Read and write (modify app permissions)

        private string TwConsumerKey = "TWITTER_CONSUMER_KEY";
        private string TwConsumerSecret = "TWITTER_CONSUMER_SECRET";
        private string TwRequestTokenUrl = "https://api.twitter.com/oauth/request_token";
        private string TwAuthorizeUrl = "https://api.twitter.com/oauth/authorize";
        private string TwAccessTokenUrl = "https://api.twitter.com/oauth/access_token";
        private string TwSaveToFile = "twitter_oauth_token.json";

        // ------------------------------------------------------------------------------------------------------
        // Quickbooks OAuth1
        private string QbConsumerKey = "QUICKBOOKS_CONSUMER_KEY";
        private string QbConsumerSecret = "QUICKBOOKS_CONSUMER_SECRET";

        // See https://developer.intuit.com/docs/0100_quickbooks_online/0100_essentials/0085_develop_quickbooks_apps/0004_authentication_and_authorization/connect_from_within_your_app#/Sample_Implementations

        private string QbRequestTokenUrl = "https://oauth.intuit.com/oauth/v1/get_request_token";
        private string QbAuthorizeUrl = "https://appcenter.intuit.com/Connect/Begin";
        private string QbAccessTokenUrl = "https://oauth.intuit.com/oauth/v1/get_access_token";
        private string QbSaveToFile = "quickbooks_oauth_token.json";

        // ------------------------------------------------------------------------------------------------------
        // Xero OAuth1
        private string XeroConsumerKey = "XERO_CONSUMER_KEY";
        private string XeroConsumerSecret = "XERO_CONSUMER_SECRET";

        private string XeroRequestTokenUrl = "https://api.xero.com/oauth/RequestToken";
        private string XeroAuthorizeUrl = "https://api.xero.com/oauth/Authorize";
        private string XeroAccessTokenUrl = "https://api.xero.com/oauth/AccessToken";
        private string XeroSaveToFile = "xero_oauth_token.json";
        //private string XeroEndpointUrl = "https://api.xero.com/api.xro/2.0/";

        // ------------------------------------------------------------------------------------------------------
        // Magento OAuth1
        private string MagentoConsumerKey = "MAGENTO_CONSUMER_KEY";
        private string MagentoConsumerSecret = "MAGENTO_CONSUMER_SECRET";


        // /oauth/initiate - this endpoint is used for retrieving the Request Token.
        // /oauth/authorize - this endpoint is used for user authorization (Customer).
        // /admin/oauth_authorize - this endpoint is used for user authorization (Admin).
        // /oauth/token - this endpoint is used for retrieving the Access Token.


        private string MagentoRequestTokenUrl = "http://magento.chilkat.io/oauth/initiate";
        // For the Authorize URL, we can choose either the Customer or Admin option.  We'll choose Customer here.
        private string MagentoAuthorizeUrl = "http://magento.chilkat.io/oauth/authorize";
        private string MagentoAccessTokenUrl = "http://magento.chilkat.io/oauth/token";
        private string MagentoSaveToFile = "magento_oauth_token.json";
        //private string MagentoEndpointUrl = "https://";

        // ------------------------------------------------------------------------------------------------------
        private string ConsumerKey = "";
        private string ConsumerSecret = "";
        private string RequestTokenUrl = "";
        private string AuthorizeUrl = "";
        private string AccessTokenUrl = "";
        private string SaveToFile = "";

        private string OAuthRequestToken = "";
        private string OAuthRequestTokenSecret = "";
        private string OAuthVerifier = "";

        private string OAuthAccessToken = "";
        private string OAuthAccessTokenSecret = "";

        private string OAuthCallbackUrl = "http://localhost:3017/";
        private int OAuthCallbackLocalPort = 3017;

        private Chilkat.Rest m_rest = null;
        private Chilkat.OAuth1 m_oauth1 = null;

        private Chilkat.Socket m_listenSocket = null;

        // We'll set this to "quickbooks", "twitter", "xero", or "magento" so we can take special action in the code that is common to all..
        private string m_providerName = "";

        // ------------------------------------------------------------------------------------------------------

        // Creates a socket and listens on port 3017 for the expected callback from the browser.
        private bool startListenSocket()
            {
            // Just in case ...
            if (m_listenSocket != null)
                {
                // We no longer need the listen socket...
                // Close it so that it's no longer listening on our port (such as port 3017)
                m_listenSocket.Close(10);
                m_listenSocket.Dispose();
                }

            Chilkat.Socket listenSock = new Chilkat.Socket();
            m_listenSocket = listenSock;

            int backLog = 5;
            bool success = listenSock.BindAndListen(OAuthCallbackLocalPort, backLog);
            if (!success)
                {
                textBox1.Text += listenSock.LastErrorText + "\r\n";
                return false;
                }

            // When the connection is accepted, the listenSock_OnTaskCompleted will be called.
            listenSock.OnTaskCompleted += listenSock_OnTaskCompleted;

            // Wait a max of 60 seconds before giving up.
            Chilkat.Task task = listenSock.AcceptNextConnectionAsync(60000);
            task.Run();

            return true;
            }

        private void fgAppendToLog(string s)
            {
            this.Invoke((MethodInvoker)delegate
            {
                textBox1.Text += s;
            });
            }


        // This is called when the connection arrives from the browser.
        // To clarify, when we called "System.Diagnostics.Process.Start(urlForBrowser);",
        // this started a browser, and the browser navigated to the given URL. This means it sent a GET request
        // to Twitter.  Twitter's response is a redirect to http://localhost:3017.  Therefore, when the browser
        // receives Twitter's response, it then sends a GET request to http://localhost:3017.  That's us.
        // This is called when the browser has made the connection.  We're acting as a temporary web server -- meaning
        // we must receive the request from the browser, we get the information we need from the request (which
        // constains our access token), and then we send an HTML response back to the browser.
        // The access token is then used for Twitter REST API calls, and it never expires (unless revoked by the owner).

        // ** Also..this callback happens on a background thread. We cannot just access UI elements directly..
        void listenSock_OnTaskCompleted(object sender, Chilkat.TaskCompletedEventArgs args)
            {
            // First of all.. let's make sure the AcceptNextConnection succeeded.
            if (!args.Task.TaskSuccess)
                {
                fgAppendToLog("Failed to accept connection.\r\n");
                fgAppendToLog(args.Task.ResultErrorText);
                return;
                }

            // The first thing to do is to get the connected socket.
            Chilkat.Socket sock = new Chilkat.Socket();
            sock.LoadTaskResult(args.Task);

            // We no longer need the listen socket...
            // Close it so that it's no longer listening on our port (such as port 3017)
            if (m_listenSocket != null)
                {
                m_listenSocket.Close(10);
                m_listenSocket = null;
                }

            // Read the start line of the request..
            string startLine = sock.ReceiveUntilMatch("\r\n");
            if (startLine == null)
                {
                fgAppendToLog("Failed to receive start line.\r\n");
                fgAppendToLog(sock.LastErrorText);
                return;
                }

            // Read the request header.
            string requestHeader = sock.ReceiveUntilMatch("\r\n\r\n");
            if (requestHeader == null)
                {
                fgAppendToLog("Failed to receive request header.\r\n");
                fgAppendToLog(sock.LastErrorText);
                return;
                }

            // The browser SHOULD be sending us a GET request, and therefore there is no body to the request.
            // Once the request header is received, we have all of it.
            // We can now send our HTTP response.

            string responseBodyHtml = "<html><body><p>Chilkat thanks you!</b></body</html>";
            sock.SendString("HTTP/1.1 200 OK\r\n");
            sock.SendString("Content-Length: " + responseBodyHtml.Length.ToString() + "\r\n");
            sock.SendString("Content-Type: text/html\r\n\r\n");
            sock.SendString(responseBodyHtml);
            sock.Close(50);

            fgAppendToLog("startLine: " + startLine + "\r\n");
            fgAppendToLog("request header:\r\n" + requestHeader);

            // The information we need is in the startLine.
            // For example, the startLine will look like this:
            //  GET /?oauth_token=abcdRQAAZZAAxfBBAAABVabcd_k&oauth_verifier=9rdOq5abcdCe6cn8M3jabcdj3Eabcd HTTP/1.1

            string queryParams = startLine.Replace("GET /?", "").Replace(" HTTP/1.1", "").Trim();

            Chilkat.Hashtable hashTab = new Chilkat.Hashtable();
            hashTab.AddQueryParams(queryParams);

            OAuthRequestToken = hashTab.LookupStr("oauth_token");
            OAuthVerifier = hashTab.LookupStr("oauth_verifier");

            // Save a few extra items for quickbooks.
            string realmId = "";
            string dataSource = "";
            if (m_providerName.Equals("quickbooks"))
                {
                realmId = hashTab.LookupStr("realmId");
                dataSource = hashTab.LookupStr("dataSource");
                }

            // ------------------------------------------------------------------------------
            // Finally , we must exchange the OAuth Request Token for an OAuth Access Token.

            Chilkat.Http http = new Chilkat.Http();

            http.OAuth1 = true;
            http.OAuthConsumerKey = ConsumerKey;
            http.OAuthConsumerSecret = ConsumerSecret;
            http.OAuthCallback = OAuthCallbackUrl;
            http.OAuthToken = OAuthRequestToken;
            http.OAuthTokenSecret = OAuthRequestTokenSecret;
            http.OAuthVerifier = OAuthVerifier;

            Chilkat.HttpRequest req = new Chilkat.HttpRequest();
            Chilkat.HttpResponse resp = null;
            resp = http.PostUrlEncoded(AccessTokenUrl, req);

            if (resp == null)
                {
                fgAppendToLog(http.LastErrorText);
                return;
                }

            fgAppendToLog("response status: " + resp.StatusCode.ToString() + "\r\n");
            if (resp.StatusCode != 200)
                {
                fgAppendToLog(resp.StatusLine + "\r\n");
                fgAppendToLog(resp.Header + "\r\n");
                fgAppendToLog(resp.BodyStr + "\r\n");
                return;
                }

            // If successful, the resp.BodyStr contains this:  
            // oauth_token=12347455-ffffrrlaBdCjbdGfyjZabcdb5APNtuTPNabcdEpp&oauth_token_secret=RxxxxJ8mTzUhwES4xxxxuJyFWDN8ZfHmrabcddh88LmWE&user_id=85123455&screen_name=chilkatsoft&x_auth_expires=0
            fgAppendToLog("final response bodyStr: " + resp.BodyStr + "\r\n");


            hashTab.Clear();
            hashTab.AddQueryParams(resp.BodyStr);

            OAuthAccessToken = hashTab.LookupStr("oauth_token");
            OAuthAccessTokenSecret = hashTab.LookupStr("oauth_token_secret");

            if ((OAuthAccessTokenSecret == null) || (OAuthAccessToken == null))
                {
                fgAppendToLog("Received unexpected or error response.\r\n");
                return;
                }

            // Save this token for re-use...
            Chilkat.JsonObject json = new Chilkat.JsonObject();
            json.AppendString("oauth_token", OAuthAccessToken);
            json.AppendString("oauth_token_secret", OAuthAccessTokenSecret);
            if (m_providerName.Equals("quickbooks"))
                {
                json.AppendString("realmId", realmId);
                json.AppendString("dataSource", dataSource);
                }
            System.IO.File.WriteAllText(SaveToFile, json.Emit());

            return;
            }

        private void begin_oauth1()
            {
            // The 1st step in 3-legged OAuth1.0a is to send a POST to the request token URL to obtain an OAuth Request Token
            Chilkat.Http http = new Chilkat.Http();

            http.OAuth1 = true;
            http.OAuthConsumerKey = ConsumerKey;
            http.OAuthConsumerSecret = ConsumerSecret;
            http.OAuthCallback = OAuthCallbackUrl;

            Chilkat.HttpRequest req = new Chilkat.HttpRequest();
            Chilkat.HttpResponse resp = null;
            resp = http.PostUrlEncoded(RequestTokenUrl, req);

            if (resp == null)
                {
                textBox1.Text = http.LastErrorText;
                return;
                }

            // If successful, the resp.BodyStr contains this:  
            // oauth_token=-Wa_KwAAAAAAxfEPAAABV8Qar4Q&oauth_token_secret=OfHY4tZBX2HK4f7yIw76WYdvnl99MVGB&oauth_callback_confirmed=true
            textBox1.Text += resp.BodyStr + "\r\n";
            
            Chilkat.Hashtable hashTab = new Chilkat.Hashtable();
            hashTab.AddQueryParams(resp.BodyStr);

            OAuthRequestToken = hashTab.LookupStr("oauth_token");
            OAuthRequestTokenSecret = hashTab.LookupStr("oauth_token_secret");

            textBox1.Text += "oauth_token = " + OAuthRequestToken + "\r\n";
            textBox1.Text += "oauth_token_secret = " + OAuthRequestTokenSecret + "\r\n";

            // ---------------------------------------------------------------------------
            // The next step is to form a URL to send to the AuthorizeUrl
            // This is an HTTP GET that we load into an embedded or popup browser.

            // Send the user to the oauth/authorize step in a web browser, including an oauth_token parameter:
            // https://api.twitter.com/oauth/authorize?oauth_token=Z6eEdO8MOmk394WozF5oKyuAv855l4Mlqo7hhlSLik

            string urlForBrowser = AuthorizeUrl + "?oauth_token=" + OAuthRequestToken;

            // When the above URL is loaded into a browser, the response from Twitter will redirect back to localhost:3017
            // (http://locahost:3017 was our http.OAuthCallback from step 1)
            // We'll need to start a socket that is listening on port 3017 for the callback from the browser.
            bool success = startListenSocket();
            if (!success)
                {
                textBox1.Text += "Failed to start listen socket.\r\n";
                return;
                }

            // Send the URL to the browser:
            // This is where the end-user should accept or deny the authorization request.
            System.Diagnostics.Process.Start(urlForBrowser);

            return;
            }

        private void btnTwitter_Click(object sender, EventArgs e)
            {
            textBox1.Text = "";

            m_providerName = "twitter";
            ConsumerKey = TwConsumerKey;
            ConsumerSecret = TwConsumerSecret;
            RequestTokenUrl = TwRequestTokenUrl;
            AuthorizeUrl = TwAuthorizeUrl;
            AccessTokenUrl = TwAccessTokenUrl;
            SaveToFile = TwSaveToFile;

            begin_oauth1();
            return;
            }

        private void btnQbOauth_Click(object sender, EventArgs e)
            {
            textBox1.Text = "";

            m_providerName = "quickbooks";
            ConsumerKey = QbConsumerKey;
            ConsumerSecret = QbConsumerSecret;
            RequestTokenUrl = QbRequestTokenUrl;
            AuthorizeUrl = QbAuthorizeUrl;
            AccessTokenUrl = QbAccessTokenUrl;
            SaveToFile = QbSaveToFile;

            begin_oauth1();
            return;
            }

        private void btnXeroOAuth_Click(object sender, EventArgs e)
            {
            textBox1.Text = "";

            m_providerName = "xero";
            ConsumerKey = XeroConsumerKey;
            ConsumerSecret = XeroConsumerSecret;
            RequestTokenUrl = XeroRequestTokenUrl;
            AuthorizeUrl = XeroAuthorizeUrl;
            AccessTokenUrl = XeroAccessTokenUrl;
            SaveToFile = XeroSaveToFile;

            begin_oauth1();
            return;
            }

        private void btnMagentoOauth_Click(object sender, EventArgs e)
            {
            textBox1.Text = "";

            m_providerName = "magento";
            ConsumerKey = MagentoConsumerKey;
            ConsumerSecret = MagentoConsumerSecret;
            RequestTokenUrl = MagentoRequestTokenUrl;
            AuthorizeUrl = MagentoAuthorizeUrl;
            AccessTokenUrl = MagentoAccessTokenUrl;
            SaveToFile = MagentoSaveToFile;

            // For Magento, we need "127.0.0.1" instead of localhost.
            string savedCallbackUrl = OAuthCallbackUrl;
            OAuthCallbackUrl = "http://127.0.0.1:3017/";

            begin_oauth1();

            OAuthCallbackUrl = savedCallbackUrl;
            return;
            }


        private void Form1_Load(object sender, EventArgs e)
            {
            Chilkat.Global glob = new Chilkat.Global();
            bool success = glob.UnlockBundle("Anything for 30-day trial");
            if (!success) MessageBox.Show("Failed to unlock Chilkat for trial.");
            }

        private bool checkInitRest()
            {
            // If non-null, we must've already set it up..
            if (m_rest != null) return true;

            if (!System.IO.File.Exists("twitter_oauth_token.json"))
                {
                MessageBox.Show("An access token must first be obtained.");
                return false;
                }

            string jsonStr = System.IO.File.ReadAllText("twitter_oauth_token.json");
            Chilkat.JsonObject json = new Chilkat.JsonObject();
            json.Load(jsonStr);


            m_rest = new Chilkat.Rest();
            m_rest.VerboseLogging = true;

            m_oauth1 = new Chilkat.OAuth1();

            m_oauth1.ConsumerKey = ConsumerKey;
            m_oauth1.ConsumerSecret = ConsumerSecret;
            m_oauth1.Token = json.StringOf("oauth_token");
            m_oauth1.TokenSecret = json.StringOf("oauth_token_secret");
            m_oauth1.SignatureMethod = "HMAC-SHA1";
            m_oauth1.GenNonce(16);
            
            textBox1.Text += m_oauth1.LastErrorText + "\r\n";
            textBox1.Text += "m_oauth1.Nonce = " + m_oauth1.Nonce + "\r\n";

            m_oauth1.OauthVersion = "1.0";

            m_rest.SetAuthOAuth1(m_oauth1, false);

            bool success = m_rest.Connect("api.twitter.com", 443, true, true);
            if (!success)
                {
                textBox1.Text += m_rest.LastErrorText + "\r\n";
                m_rest = null;
                return false;
                }

            return true;
            }

        // Your Twitter Application needs the "Read, Write and Access direct messages" permission/access to post status updates (i.e. tweet)
        private void btnTweet_Click(object sender, EventArgs e)
            {
            textBox1.Text = "";

            if (!checkInitRest()) return;

            m_rest.ClearAllQueryParams();
            m_rest.AddQueryParam("status","This is a test tweet.");
            string response = m_rest.FullRequestFormUrlEncoded("POST", "/1.1/statuses/update.json");
            if (!m_rest.LastMethodSuccess)
                {
                textBox1.Text = m_rest.LastErrorText;
                return;
                }
            textBox1.Text += m_rest.LastErrorText + "\r\n";

            textBox1.Text += response + "\r\n";

            return;
            }


        }
    }
