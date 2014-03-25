using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitterAvailability.Dto;
using TwitterAvailability.Repository;

namespace TwitterAvailability.Test
{
    [TestClass]
    public class UnitTest3
    {
        [TestMethod]
        public void InsertDetail()
        {

            FileStream fs = File.OpenRead(@"C:\Users\Jacobeo\Pictures\amarillo.png");
            Byte[] bytes = null;

            BinaryReader br = new BinaryReader(fs);
            bytes = br.ReadBytes((Int32)fs.Length);

            IssueDetailRepository issueDetailRepository = new IssueDetailRepository();
            IssueDetail issueDetail = new IssueDetail()
                                          {
                                              ContentType = "myContent",
                                              Data = bytes,
                                              Date = DateTime.Now,
                                              Description = "Description",
                                              FileName = "fileName",
                                              InternalId = "InternalId"
                                          };


            issueDetail = issueDetailRepository.SaveEntity(issueDetail);

            Int16 a = 11;

        }

        [TestMethod]
        public void SelectDetail()
        {
            IssueDetailRepository issueDetailRepository = new IssueDetailRepository();

            IList<IssueDetail> res = issueDetailRepository.FindAllByInternalId("InternalId");
            Byte[] issueDetail = issueDetailRepository.GetDataFromUniqueId(1);

        }
  

     
    }
}
