﻿namespace Readme.Logic.DomainModel.LineModels
{
    public class ActionDto
    {
        public string type { get; set; } //1
        public string label { get; set; } //2

        //confirmtemplate
        public string text { get; set; } //2 3

        //carouseltemplate
        public string data { get; set; } //3 
        public string uri { get; set; } //3


        //imagemap
        public string linkUri { get; set; } //2
        public AreaDto area { get; set; } //3


        //public string action { get; set; }/
    }
}
