using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace TwitterClient
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            WebClient twitterclient = new WebClient();
            twitterclient.DownloadStringCompleted +=
                new DownloadStringCompletedEventHandler(twitterclient_DownloadStringCompleted);
            twitterclient.DownloadStringAsync(
                new Uri("http://api.twitter.com/1/statuses/user_timeline.xml?screen_name=" + UserName.Text));
        }
        void twitterclient_DownloadStringCompleted(object sendar, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }
            XElement xmlTweets = XElement.Parse(e.Result);
            listBox1.ItemsSource =
                from tweet in xmlTweets.Descendants("status")
                select new TwitterItems
                {
                    ImageSource =
                        tweet.Element("user").Element("profile_image_url").Value,
                    Message = tweet.Element("text").Value,
                    UserName = tweet.Element("user").Element("screen_name").Value
                };
        }
    }
}
