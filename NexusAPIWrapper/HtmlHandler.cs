using AngleSharp.Dom;
using AngleSharp.Text;
using CsQuery;
using CsQuery.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

namespace NexusAPIWrapper
{
    public class HtmlHandler
    {
        public HtmlHandler() { }

        internal string GetJQuerySelectorStringForTransformedBodyHtml(string h5Header)
        {
            /*
             * In this method we insert the selectors for the different medcom messages.
             * It has been separated by regions
             * */
            string result = string.Empty;
            switch (h5Header.ToLower())
            {
                #region udskrivningsrapport selectors
                case "fremtidige aftaler":
                    result = "h5:contains(\"" + h5Header + "\") + table > tbody";
                    break;
                case "aftaler omkring kost første døgn efter udskrivning":
                    result = "h5:contains(\"" + h5Header + "\") + table > tbody";
                    break;
                case "medicin information relateret til udskrivning":
                    result = "h5:contains(\"" + h5Header + "\") + table > tbody";
                    break;
                case "seneste medicingivning":
                    result = "h5:contains(\"" + h5Header + "\") + table > tbody";
                    break;
                case "sygeplejefaglige problemområder":
                    result = "h5:contains(\"" + h5Header + "\") + table > tbody";
                    break;
                case "diagnoser":
                    result = "h5:contains(\"" + h5Header + "\") + table > tbody > tr > td > table";
                    break;
                case "aktuel indlæggelse":
                    result = "h5:contains(\"" + h5Header + "\") + table > tbody";
                    break;
                case "pårørende/relationer":
                    result = "h5:contains(\"" + h5Header + "\")";
                    break;
                case "funktionsevner ved udskrivelse":
                    result = "h5:contains(\"" + h5Header + "\") + table > tbody > tr > td > table > tbody";
                    break;
                    #endregion  udskrivningsrapport selectors 

            }

            return result;
        }

        internal CQ GetDecodedHTMLWithoutUnwantedTags(string outerHTML)
        {
            //decode outer HTML to have æ, ø and å working
            string decodedOuterHTML = DecodeHTML(outerHTML);
            //remove all line breaks, bold tags etc.
            decodedOuterHTML = decodedOuterHTML.Replace(" <br>", ".\n");
            decodedOuterHTML = decodedOuterHTML.Replace("<br>", ".\n");
            decodedOuterHTML = decodedOuterHTML.Replace("</br>", "");
            decodedOuterHTML = decodedOuterHTML.Replace("<b>", "");
            decodedOuterHTML = decodedOuterHTML.Replace("</b>", "");
            decodedOuterHTML = decodedOuterHTML.Replace("<p>", "");
            decodedOuterHTML = decodedOuterHTML.Replace("</p>", "");
            decodedOuterHTML = decodedOuterHTML.Replace("<q>", "");
            decodedOuterHTML = decodedOuterHTML.Replace("</q>", "");
            decodedOuterHTML = decodedOuterHTML.Replace("<hr>", "");
            decodedOuterHTML = decodedOuterHTML.Replace("</hr>", "");
            decodedOuterHTML = decodedOuterHTML.Replace("<strong>", "");
            decodedOuterHTML = decodedOuterHTML.Replace("</strong>", "");
            decodedOuterHTML = decodedOuterHTML.Replace("<h1>", "");
            decodedOuterHTML = decodedOuterHTML.Replace("</h1>", "");
            decodedOuterHTML = decodedOuterHTML.Replace("<h2>", "");
            decodedOuterHTML = decodedOuterHTML.Replace("</h2>", "");
            decodedOuterHTML = decodedOuterHTML.Replace("<h3>", "");
            decodedOuterHTML = decodedOuterHTML.Replace("</h3>", "");
            decodedOuterHTML = decodedOuterHTML.Replace("<h4>", "");
            decodedOuterHTML = decodedOuterHTML.Replace("</h4>", "");
            decodedOuterHTML = decodedOuterHTML.Replace("<h5>", "");
            decodedOuterHTML = decodedOuterHTML.Replace("</h5>", "");
            decodedOuterHTML = decodedOuterHTML.Replace("<h6>", "");
            decodedOuterHTML = decodedOuterHTML.Replace("</h6>", "");
            decodedOuterHTML = decodedOuterHTML.Replace("<!--...-->", "");
            //create a new dom object from the remaining HTML
            return CreateDomObject(decodedOuterHTML);
        }
        internal CQ CreateDomObject(string HTMLString)
        {
            return CsQuery.CQ.Create(HTMLString);
        }
        internal CQ GetHtmlBasedOnJQuerySelectorString(string HTMLString, string JQuerySelectorString)
        {
            CsQuery.CQ dom = CreateDomObject(HTMLString);
            return dom[JQuerySelectorString];
        }

        /// <summary>
        /// Returns a string result that can be parsed for specific information
        /// </summary>
        /// <param name="html"></param>
        /// <param name="h5HeaderStart"></param>
        /// <param name="h5HeaderEnd"></param>
        /// <returns></returns>
        public string GetResultFromHtml(string html, string h5HeaderStart, string h5HeaderEnd=null)
        {
            if (h5HeaderEnd == null)
            {
                string JQuerySelectorString = GetJQuerySelectorStringForTransformedBodyHtml(h5HeaderStart);
                var htmlFromJQuerySelector = GetHtmlBasedOnJQuerySelectorString(html, JQuerySelectorString);
                var node = htmlFromJQuerySelector.Elements.First().OuterHTML;
                
                return DecodeHTML(node);
                //return htmlFromJQuerySelector.Elements.First().FirstChild.NodeValue;
            }
            else
            {
                return GetResultFromHtmlSelectNextUntil(html,h5HeaderStart, h5HeaderEnd);
            }
            
        }
        public string DecodeHTML(string stringOuterHTML)
        {
            return HttpUtility.HtmlDecode(stringOuterHTML);
        }
        /// <summary>
        /// Gathers all elements from the start header to the end header, in case elements are not within a specific HTML-tag.
        /// </summary>
        /// <param name="html"></param>
        /// <param name="h5HeaderStart"></param>
        /// <param name="h5HeaderEnd"></param>
        /// <returns></returns>
        public string GetResultFromHtmlSelectNextUntil(string html, string h5HeaderStart, string h5HeaderEnd, string elementType = "table")
        {
            bool continueLooping = true;
            string JQuerySelectorString = GetJQuerySelectorStringForTransformedBodyHtml(h5HeaderStart);
            string result = string.Empty;

            while (continueLooping)
            {
                var htmlFromJQuerySelector = GetHtmlBasedOnJQuerySelectorString(html, JQuerySelectorString);

                try
                {
                    

                    if (htmlFromJQuerySelector.Elements.Count() != 0)
                    {
                        var node = htmlFromJQuerySelector.Elements.First().OuterHTML;
                        node = DecodeHTML(node);
                        if (!node.Contains(h5HeaderEnd))
                        {
                            if (result == string.Empty)
                            {
                                result = node;
                            }
                            else
                            {
                                result += node;
                            }
                            JQuerySelectorString += " + " + elementType;
                        }
                        else
                        {
                            continueLooping = false; break;
                        }
                    }
                    else
                    {
                        continueLooping = false; break;
                    }
                    
                }
                catch (Exception)
                {
                    // If the process reaches this step, it is because no more tables are returned
                    // or the sequene contains no elements
                    break;
                }
                
            }

            return result;
        }
    }
}
