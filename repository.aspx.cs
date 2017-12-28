using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

public partial class repository : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hplBack.Attributes.Add("onClick", "javascript:history.back(); return false;");
            requestRepo(0);
        }

        ViewPageIndex.PageIndexChange += ViewPageIndex_PageIndexChange;
    }


    public class Repo
    {
        public string repo_name { get; set; }
        public string full_name { get; set; }
        public string desc { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string pushed_at { get; set; }
        public string size { get; set; }
        public string star_count { get; set; }
        public string watchers_count { get; set; }
        public string language { get; set; }
        public string clone_url { get; set; }
        public string default_branch { get; set; }
        public string login { get; set; }
    }



    void ViewPageIndex_PageIndexChange(int pageIndex)
    {
        requestRepo(pageIndex);
    }



    public void requestRepo(int pageIndex)
    {
        string name = this.RouteData.Values["user"].ToString();
        lbUser.Text = name + " / ";
        HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("https://api.github.com/users/" + name + "/repos?type=all&client_id=c188d52f7d68f864b310&client_secret=209e6c6043081dd896a64c0810ef5368c06c1c60");
        req.UserAgent = @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.106 Safari/537.36";
        req.Method = "GET";
        HttpWebResponse res = (HttpWebResponse)req.GetResponse();
        StreamReader streamReader = new StreamReader(res.GetResponseStream());
        string json = "{ 'items' : " + streamReader.ReadToEnd().Replace("\n", "") + " } ";
        JObject obj = JObject.Parse(json);

        List<Repo> objRepo = new List<Repo>();
        Repo item;

        foreach (var i in obj["items"].Children())
        {
            item = new Repo();
            item.repo_name = i["name"].ToString();
            item.full_name = i["full_name"].ToString();
            item.desc = i["description"].ToString();
            if (item.desc.Length > 80)
            {
                item.desc = item.desc.Substring(0, 80) + "...";
            }
            item.created_at = i["created_at"].ToString();
            item.updated_at = i["updated_at"].ToString();
            item.pushed_at = i["pushed_at"].ToString();
            item.size = i["size"].ToString();
            item.star_count = i["stargazers_count"].ToString();
            item.watchers_count = i["watchers_count"].ToString();
            item.language = i["language"].ToString();
            item.clone_url = i["clone_url"].ToString();
            item.default_branch = i["default_branch"].ToString();
            item.login = i["owner"]["login"].ToString();

            objRepo.Add(item);
        }

        graphViews(obj);
        PagedDataSource pds = new PagedDataSource();
        pds.DataSource = objRepo;
        pds.PageSize = 12;
        pds.AllowPaging = true;
        pds.CurrentPageIndex = pageIndex;
        rptRepo.DataSource = pds;
        ViewPageIndex.SetPage(pageIndex, pds.PageCount);
        rptRepo.DataBind();
        ViewPageIndex.SetPage(pageIndex, pds.PageCount);
        ViewPageIndex.Visible = (rptRepo.Items.Count > 0);
        lbPage.Visible = (rptRepo.Items.Count > 0);
    }



    protected void rptRepo_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HiddenField hdnLogin = (HiddenField)(e.Item.FindControl("hdnLogin"));
        HiddenField hdnRepoName = (HiddenField)(e.Item.FindControl("hdnRepoName"));
        HiddenField hdnBranch = (HiddenField)(e.Item.FindControl("hdnBranch"));
        HyperLink hplDownload = (HyperLink)(e.Item.FindControl("hplDownload"));
        hplDownload.NavigateUrl = "https://codeload.github.com/" + hdnLogin.Value + "/" + hdnRepoName.Value + "/zip/" + hdnBranch.Value;
    }


    public void graphViews(JObject obj)
    {
        string a = "";
        string id = "";
        int k = 1;
        foreach (var i in obj["items"].Children())
        {
            a = a + "{" + "Subfile_title" + ":" + "'" + i["name"] + "'" + "," + "name" + ":" + "'" + i["name"] + "'" + "," + "'watchers_count" + i["name"] + "':" + "'" + i["watchers_count"] + "'" + "}" + ",".ToString();
            id = id + "{" + "'balloonText': '[[name]]: <b>[[value]]</b>'," + "'columnWidth': 0.7," + "'fillAlphas': 0.5," + "'id':" + "'" + k + "'" + "," + "'title':" + "'" + i["name"] + "'" + "," + "'type': 'column'," + "'valueField':" + "'watchers_count" + i["name"] + "'},".ToString();
            k++;
        }

        var amchart = "AmCharts.ready(function() {" +
              "AmCharts.makeChart('chartdiv', {" +
              "'type': 'serial'," +
              "'categoryField': 'Subfile_title'," +
              "'angle': 30," +
              "'depth3D': 30," +
              "'fontSize': 12," +
              "'fontFamily': 'Tahoma'," +
              "'startDuration': 1," +
              "'categoryAxis': {" +
              "'autoRotateAngle': -35," +
              "'autoRotateCount': 1," +
              "'gridPosition': 'start' }," +
              "'trendLines': []," +
              "'graphs': [ " +
              id +
              " ]," +
              "'guides': []," +
              "'valueAxes': [ { " +
              "'id': 'ValueAxis-1'," +
              "'stackType': 'regular' } ]," +
              "'allLabels': []," +
              "'balloon': {}," +
              "'legend': {" +
              "'enabled': true," +
              "'color': '#000000'," +
              "'useGraphSettings': true" +
              "}," +
              "'titles': [{" +
              "'id': 'Title-1'," +
              "'size': 12," +
              "'text': 'The number of viewers'}]," +
              "'dataProvider': ds2" +
              "});})";

        string graph2 = a;
        string jsn2 = "var ds2" + " = [" + graph2 + "];";
        System.Web.UI.ScriptManager.RegisterStartupScript(this, this.GetType(), "2", "<script>" + jsn2 + amchart + "</script>", false);
    }
}