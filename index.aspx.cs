using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

public partial class _index : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ViewPageIndex.PageIndexChange += ViewPageIndex_PageIndexChange;
    }


    public class Person
    {
        public string user { get; set; }
        public string avatar { get; set; }
        public string url { get; set; }
        public string score { get; set; }
    }


    void ViewPageIndex_PageIndexChange(int pageIndex)
    {
        requestUser(pageIndex);
    }


    public void requestUser(int pageIndex)
    {
        var name = txtSearch.Text;
        HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://api.github.com/search/users?q=" + name + "&client_id=c188d52f7d68f864b310&client_secret=209e6c6043081dd896a64c0810ef5368c06c1c60");
        req.UserAgent = @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36";
        req.Method = "GET";
        try
        {
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            StreamReader streamReader = new StreamReader(res.GetResponseStream());
            string json = streamReader.ReadToEnd().Replace("\n", "");
            JObject obj = JObject.Parse(json);

            List<Person> objPerson = new List<Person>();
            Person item;

            foreach (var i in obj["items"].Children())
            {
                item = new Person();
                item.user = i["login"].ToString();
                item.avatar = i["avatar_url"].ToString();
                item.url = i["html_url"].ToString();
                item.score = i["score"].ToString();

                objPerson.Add(item);
            }

            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = objPerson;
            pds.PageSize = 12;
            pds.AllowPaging = true;
            pds.CurrentPageIndex = pageIndex;
            rptUser.DataSource = pds;
            ViewPageIndex.SetPage(pageIndex, pds.PageCount);
            rptUser.DataBind();
            ViewPageIndex.SetPage(pageIndex, pds.PageCount);
            ViewPageIndex.Visible = (rptUser.Items.Count > 0);
            lbPage.Visible = (rptUser.Items.Count > 0);
            divValid.Visible = false;
        }
        catch
        {
            divValid.Visible = true;
        }

    }


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        requestUser(0);
    }
}