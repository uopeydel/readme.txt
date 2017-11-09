using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using SBX509;
using SBXMLAdESIntf;
using SBXMLCore;
using SBXMLSec;
using SBXMLSig;
using SBXMLTransform;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.IO;
using System.Net.Http.Headers;
using System.Net;
using WebApplication1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class SignController : Controller
    {

        [HttpGet("ResponseSignXML")]
        public HttpResponseMessage GetDocSignXML()
        {
            
            //var path = System.IO.File.ReadAllLines(@"Json\InputDataForGenXML.Json");
            var serialized = System.IO.File.ReadAllText(@"Json\InputDataForGenXML.Json");
            //var path = HttpContext.Current.Server.MapPath(@"~\App_Data\InputDataForGenXML.Json");

            //string receivedData = "";// File.ReadAllText(path);
            //JObject jsonString = JObject.Parse(receivedData);
            //string serialized = JsonConvert.SerializeObject((dynamic)jsonString);
            ETaxModels etaxData = JsonConvert.DeserializeObject<ETaxModels>(serialized);
            byte[] data = DrawWithSigner(etaxData);


            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

            //if (data != null && data.Length > 0)
            //{
            //    response.Content = new StreamContent(new MemoryStream(data));
            //    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            //    response.Content.Headers.ContentDisposition.FileName = "XMLEtax_" + DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss:sss") + "_.xml";
            //    response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
            //}
            //else
            //{
            //    response = new HttpResponseMessage(HttpStatusCode.NotFound);
            //}

            return response;
        }



        private byte[] DrawWithSigner(ETaxModels etaxData)
        {
            TElXMLDOMDocument xmlDom = new TElXMLDOMDocument();

            #region Generate Element

            #region Tag Root
            var rootElement = xmlDom.CreateElement("rsm:TaxInvoice_CrossIndustryInvoice");
            rootElement.SetAttribute("xmlns:ram", "urn:etda:uncefact:data:standard:TaxInvoice_ReusableAggregateBusinessInformationEntity:2");
            rootElement.SetAttribute("xmlns:rsm", "urn:etda:uncefact:data:standard:TaxInvoice_CrossIndustryInvoice:2");
            rootElement.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance");
            rootElement.SetAttribute("xsi:schemaLocation", "urn:etda:uncefact:data:standard:TaxInvoice_CrossIndustryInvoice:2 file:../data/standard/TaxInvoice_CrossIndustryInvoice_2p0.xsd");


            #endregion

            #region Append Tag Child

            for (int i = etaxData.Data.Count - 1; i > 0; i--)
            {
                etaxData.Data[i].XmlElement = xmlDom.CreateElement(string.Format("{0}:{1}", etaxData.Data[i].NameSpace, etaxData.Data[i].Name));
                //etaxData.Data[i].XmlElement.SetAttribute("index", "num " + i + " " + etaxData.Data[i].Index);

                #region SetAttribute
                foreach (var attbr in etaxData.Data[i].Attribute)
                {
                    etaxData.Data[i].XmlElement.SetAttribute(attbr.Key, attbr.Value);
                }
                #endregion

                if (etaxData.Data[i].IsLeaf && !string.IsNullOrEmpty(etaxData.Data[i].InnerText))
                {
                    etaxData.Data[i].XmlElement.InnerXML = etaxData.Data[i].InnerText;
                }
                else if (!etaxData.Data[i].IsLeaf)
                {
                    //find a leaf
                    var LeafTag = etaxData.Data
                        .Where(w =>
                        //find only child
                        w.Index.StartsWith(etaxData.Data[i].Index)
                        &&
                        //without child of child
                        w.Index.Length - 3 <= etaxData.Data[i].Index.Length
                        &&
                        //Not find self
                        w.Index != etaxData.Data[i].Index
                        )
                        .OrderBy(o => Int32.Parse(o.Index.Split('.').LastOrDefault()))
                        .ToList();
                    foreach (var childLeaf in LeafTag)
                    {
                        etaxData.Data[i].XmlElement.AppendChild(childLeaf.XmlElement);
                    }
                }
            }
            #endregion

            #region Append All Child To Root
            foreach (var exd in etaxData.Data.Where(w => w.Index.Length == 1).ToArray())
            {
                rootElement.AppendChild(exd.XmlElement);
            }

            xmlDom.AppendChild(rootElement);
            //XmlSignerGen(xmlDom,"fileCertPath","123456");
            #endregion

            xmlDom.SaveToFile("C:\\Users\\ton\\Downloads\\xampledomwithsigner.xml");
            return null;


            //Stream stem = new MemoryStream();
            //xmlDom.SaveToStream(stem);
            //return ((MemoryStream)stem).ToArray();
            #endregion
        }
 

    }
}
