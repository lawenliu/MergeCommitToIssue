using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MergeCommitToIssue
{
    public partial class MergeMain : Form
    {
        private static string Project_Cassandra = "cassandra";
        private static string Project_ElasticSearch = "elasticsearch";
        private static string Project_Camel = "camel";
        private static string ProjectName = Project_Cassandra;
        public MergeMain()
        {
            InitializeComponent();
        }
        
        private void MergeMain_Load(object sender, EventArgs e)
        {
            cbProjectName.Items.Add(Project_Cassandra);
            cbProjectName.Items.Add(Project_ElasticSearch);
            cbProjectName.Items.Add(Project_Camel);
            cbProjectName.SelectedItem = ProjectName;
        }
        
        private void cbProjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProjectName = cbProjectName.SelectedItem.ToString();
            tbIssueFolder.Text = string.Format(@"D:\lxl\{0}\issues", ProjectName);
            tbCommitFolder.Text = string.Format(@"D:\lxl\{0}\commits", ProjectName);
            tbTargetFolder.Text = string.Format(@"D:\lxl\{0}\merged", ProjectName);
        }

        private void btnIssueFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = tbIssueFolder.Text;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                tbIssueFolder.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnCommitFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = tbCommitFolder.Text;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                tbCommitFolder.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnTargetFolder_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = tbTargetFolder.Text;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                tbTargetFolder.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            MergeCommitToIssue();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MergeCommitToIssue()
        {
            Dictionary<string, List<string>> commitPosHashMap = getCommitHashMap();
            //CommitInfo commitInfo = getCommitInfoByPos("D:\\lxl\\elasticsearch\\commits\\elasticsearchCommit1.txt+114586");
            string[] fileEntries = Directory.GetFiles(tbIssueFolder.Text);
            foreach (string fileName in fileEntries)
            {
                DealWithOneIssueFile(fileName, commitPosHashMap);
            }

            MessageBox.Show("Merge Done!");
        }

        private void DealWithOneIssueFile(string fileName, Dictionary<string, List<string>> commitPosHashMap)
        {
            XmlWriter xmlWriter = XmlWriter.Create(tbTargetFolder.Text + "\\" + Path.GetFileName(fileName));
            XmlReader xmlReader = XmlReader.Create(fileName);
            string itemSummary = string.Empty;
            string itemKey = string.Empty;
            bool startItem = false;
            bool startSummary = false;
            bool startKey = false;
            while (xmlReader.Read())
            {
                switch (xmlReader.NodeType)
                {
                    case XmlNodeType.CDATA:
                        xmlWriter.WriteCData(xmlReader.Value);
                        break;
                    case XmlNodeType.Comment:
                        xmlWriter.WriteComment(xmlReader.Value);
                        break;
                    case XmlNodeType.Whitespace:
                        xmlWriter.WriteWhitespace(xmlReader.Value);
                        break;
                    case XmlNodeType.Element:
                        xmlWriter.WriteStartElement(xmlReader.Name);
                        if (xmlReader.Name.Equals("item"))
                        {
                            startItem = true;
                        }

                        if (xmlReader.Name.Equals("summary") && startItem)
                        {
                            startSummary = true;
                        }

                        if (xmlReader.Name.Equals("key") && startItem)
                        {
                            startKey = true;
                        }

                        bool isEmptyElement = xmlReader.IsEmptyElement;
                        while (xmlReader.MoveToNextAttribute())
                        {
                            xmlWriter.WriteAttributeString(xmlReader.Name, xmlReader.Value);
                        }

                        if (isEmptyElement)
                        {
                            xmlWriter.WriteEndElement();
                        }

                        break;
                    case XmlNodeType.Text:
                        xmlWriter.WriteString(xmlReader.Value);
                        if (startSummary)
                        {
                            itemSummary = xmlReader.Value;
                            startSummary = false;
                        }

                        if (startKey)
                        {
                            itemKey = xmlReader.Value;
                            startKey = false;
                        }

                        break;
                    case XmlNodeType.EndElement:
                        // Write commit information of this item before close this item.
                        if (xmlReader.Name.Equals("item"))
                        {
                            List<CommitInfo> commitInfos = new List<CommitInfo>();
                            if (commitPosHashMap.ContainsKey(itemSummary))
                            {
                                commitInfos = getCommitInfo(commitPosHashMap[itemSummary]);
                                Console.WriteLine(itemSummary);
                            }
                            if (commitPosHashMap.ContainsKey(itemKey))
                            {
                                commitInfos = getCommitInfo(commitPosHashMap[itemKey]);
                                Console.WriteLine(itemKey);
                            }

                            xmlWriter.WriteStartElement("commits");
                            foreach(CommitInfo commitInfo in commitInfos)
                            {
                                // Write Commit
                                xmlWriter.WriteStartElement("commit");
                                // Write Files
                                xmlWriter.WriteStartElement("files");
                                foreach(string file in commitInfo.Files)
                                {
                                    xmlWriter.WriteStartElement("file");
                                    xmlWriter.WriteString(file);
                                    xmlWriter.WriteEndElement();
                                }
                                xmlWriter.WriteEndElement();
                                // Write Comments
                                xmlWriter.WriteStartElement("comments");
                                foreach (string comment in commitInfo.Comments)
                                {
                                    xmlWriter.WriteStartElement("comment");
                                    try
                                    {
                                        xmlWriter.WriteString(comment);
                                    } catch(Exception) { }
                                    xmlWriter.WriteEndElement();
                                }
                                xmlWriter.WriteEndElement();
                                xmlWriter.WriteEndElement();
                            }

                            xmlWriter.WriteEndElement();

                            itemSummary = string.Empty;
                            itemKey = string.Empty;
                            startItem = false;
                        }

                        xmlWriter.WriteEndElement();
                        break;
                    default:
                        MessageBox.Show(xmlReader.NodeType.ToString());
                        break;
                }
            }

            xmlReader.Close();
            xmlWriter.Close();
        }

        private List<CommitInfo> getCommitInfo(List<string> commitPositions)
        {
            List<CommitInfo> commitInfos = new List<CommitInfo>();

            foreach (string commit in commitPositions)
            {
                commitInfos.Add(getCommitInfoByPos(commit));
            }

            return commitInfos;
        }

        private List<CommitInfo> getCommitInfo1(string title)
        {
            List<CommitInfo> commitInfos = new List<CommitInfo>();
            string[] fileEntries = Directory.GetFiles(tbCommitFolder.Text);
            foreach (string fileName in fileEntries)
            {
                StreamReader sr = new StreamReader(fileName);
                CommitInfo commitInfo = null;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (line.StartsWith("time:"))
                    {
                        sr.ReadLine();
                        line = sr.ReadLine();
                        if (line.StartsWith("class:"))
                        {
                            commitInfo = new CommitInfo();
                            List<string> files = new List<string>();
                            line = sr.ReadLine();
                            while (!sr.EndOfStream && !line.StartsWith("comments"))
                            {
                                files.Add(line);
                                line = sr.ReadLine();
                            }

                            commitInfo.Files = files;
                            line = sr.ReadLine(); // Read first comments to compare title
                            if (line.Equals(title))
                            {
                                List<string> comments = new List<string>();
                                line = sr.ReadLine();
                                while (!sr.EndOfStream && !string.IsNullOrEmpty(line.Trim()))
                                {
                                    comments.Add(line);
                                    line = sr.ReadLine();
                                }

                                commitInfo.Comments = comments;
                                commitInfos.Add(commitInfo);
                                MessageBox.Show("Find matched commit!");
                            }

                        }
                    }
                }
            }

            return commitInfos;
        }

        private Dictionary<string, List<string>> getCommitHashMap()
        {
            Dictionary<string, List<string>> commitPositions = new Dictionary<string, List<string>>();
            string[] fileEntries = Directory.GetFiles(tbCommitFolder.Text);
            foreach (string fileName in fileEntries)
            {
                FileStream fs = File.OpenRead(fileName);
                StreamReader sr = new StreamReader(fs);
                string line = sr.ReadLine();
                long position = line.Length + 2;
                long start = 0;
                while (!sr.EndOfStream)
                {
                    if (line.StartsWith("time:"))
                    {
                        start = position;
                        fs.Seek(position, SeekOrigin.Begin);
                        sr = new StreamReader(fs);
                        line = sr.ReadLine();
                        position += line.Length + 2;
                        if (!line.StartsWith("2"))
                        {
                            //MessageBox.Show("Error here!");
                        }
                    }

                    if (line.StartsWith("comments:"))
                    {
                        line = sr.ReadLine();
                        position += line.Length + 2;
                        fs.Seek(position, SeekOrigin.Begin);
                        sr = new StreamReader(fs);
                        string comment = line;
                        bool hasAddedID = false;
                        while (!sr.EndOfStream && !line.StartsWith("time:"))
                        {
                            // 1. For CASSANDRA project
                            int index = line.IndexOf("CASSANDRA-"); 
                            if (ProjectName == Project_Cassandra && index >= 0)
                            {
                                int end = index + 10;
                                while (end < line.Length && line[end] >= '0' && line[end] <= '9')
                                {
                                    end++;
                                }

                                if (end > index)
                                {
                                    comment = line.Substring(index, end - index);

                                    if (!commitPositions.ContainsKey(comment))
                                    {
                                        commitPositions.Add(comment, new List<string>());
                                    }

                                    commitPositions[comment].Add(fileName + "+" + start);
                                    hasAddedID = true;
                                }
                            }

                            // 2. For Github projects
                            index = line.IndexOf("#");
                            if (ProjectName == Project_ElasticSearch && index >= 0)
                            {
                                while (index  < line.Length)
                                {
                                    index = index + 1; // ignore '#'
                                    int end = index;
                                    while (end < line.Length && line[end] >= '0' && line[end] <= '9')
                                    {
                                        end++;
                                    }

                                    if (end > index)
                                    {
                                        comment = line.Substring(index, end - index);

                                        if (!commitPositions.ContainsKey(comment))
                                        {
                                            commitPositions.Add(comment, new List<string>());
                                        }

                                        commitPositions[comment].Add(fileName + "+" + start);
                                        hasAddedID = true;
                                    }

                                    index = end;
                                    while (index < line.Length && line[index] != '#')
                                    {
                                        index++;
                                    }
                                }
                            }

                            // 3. For CAMEL
                            index = line.IndexOf("CAMEL-");
                            if (ProjectName == Project_Camel && index >= 0)
                            {
                                int end = index + 6;
                                while (end < line.Length && line[end] >= '0' && line[end] <= '9')
                                {
                                    end++;
                                }

                                if (end > index)
                                {
                                    comment = line.Substring(index, end - index);

                                    if (!commitPositions.ContainsKey(comment))
                                    {
                                        commitPositions.Add(comment, new List<string>());
                                    }

                                    commitPositions[comment].Add(fileName + "+" + start);
                                    hasAddedID = true;
                                }
                            }

                            line = sr.ReadLine();
                            if (line != null)
                            {
                                position += line.Length + 2;
                                fs.Seek(position, SeekOrigin.Begin);
                                sr = new StreamReader(fs);
                            }
                        }

                        if (!hasAddedID)
                        {
                            if (!commitPositions.ContainsKey(comment))
                            {
                                commitPositions.Add(comment, new List<string>());
                            }

                            commitPositions[comment].Add(fileName + "+" + start);
                        }
                    }

                    if (line != null && !line.StartsWith("time:"))
                    {
                        line = sr.ReadLine();
                        if (line != null)
                        {
                            position += line.Length + 2;
                            fs.Seek(position, SeekOrigin.Begin);
                            sr = new StreamReader(fs);
                        }
                    }
                }

                sr.Close();
                fs.Close();
            }

            return commitPositions;
        }

        private CommitInfo getCommitInfoByPos(string positionInfo)
        {
            string[] pis = positionInfo.Split('+');
            string fileName = pis[0];
            long position = long.Parse(pis[1]);
            FileStream fs = File.OpenRead(fileName);
            fs.Position = position;
            StreamReader sr = new StreamReader(fs);
            CommitInfo commitInfo = new CommitInfo();
            string line = sr.ReadLine();

            while(!sr.EndOfStream && !line.StartsWith("time:"))
            {
                if (line.StartsWith("class:"))
                {
                    commitInfo = new CommitInfo();
                    List<string> files = new List<string>();
                    line = sr.ReadLine();
                    while (!sr.EndOfStream && !line.StartsWith("comments"))
                    {
                        files.Add(line);
                        line = sr.ReadLine();
                    }

                    commitInfo.Files = files;

                    List<string> comments = new List<string>();
                    line = sr.ReadLine();
                    while (!sr.EndOfStream && !string.IsNullOrEmpty(line.Trim()))
                    {
                        comments.Add(line);
                        line = sr.ReadLine();
                    }

                    commitInfo.Comments = comments;
                    break;
                }

                line = sr.ReadLine();
            }

            if (commitInfo.Files == null || commitInfo.Comments == null)
            {
                MessageBox.Show("CommitInfo has error!");
            }

            return commitInfo;
        }
    }
}
