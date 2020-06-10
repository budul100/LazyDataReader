namespace StandardTrafficNetworkWithRoot
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    // [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://intf.mb.ivu.de/")]
    public partial class NetworkPointArea
    {
        #region Private Fields

        private string descriptionField;
        private NetworkPointAreaKey networkPointAreaKeyField;
        private Validity validityField;

        #endregion Private Fields

        #region Public Properties

        /// <remarks/>
        public string description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public NetworkPointAreaKey networkPointAreaKey
        {
            get
            {
                return this.networkPointAreaKeyField;
            }
            set
            {
                this.networkPointAreaKeyField = value;
            }
        }

        /// <remarks/>
        public Validity validity
        {
            get
            {
                return this.validityField;
            }
            set
            {
                this.validityField = value;
            }
        }

        #endregion Public Properties
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    // [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://intf.mb.ivu.de/")]
    public partial class NetworkPointAreaKey
    {
        #region Private Fields

        private string abbreviationField;

        private string divisionAbbreviationField;
        private string externalNumberField;

        private string typeField;

        #endregion Private Fields

        #region Public Properties

        /// <remarks/>
        public string abbreviation
        {
            get
            {
                return this.abbreviationField;
            }
            set
            {
                this.abbreviationField = value;
            }
        }

        /// <remarks/>
        public string divisionAbbreviation
        {
            get
            {
                return this.divisionAbbreviationField;
            }
            set
            {
                this.divisionAbbreviationField = value;
            }
        }

        /// <remarks/>
        public string externalNumber
        {
            get
            {
                return this.externalNumberField;
            }
            set
            {
                this.externalNumberField = value;
            }
        }

        /// <remarks/>
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        #endregion Public Properties
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    // [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://intf.mb.ivu.de/")]
    // [System.Xml.Serialization.XmlRootAttribute("trafficNetwork", Namespace = "http://intf.mb.ivu.de/", IsNullable = false)]
    [System.Xml.Serialization.XmlRootAttribute("trafficNetwork", IsNullable = true)]
    public partial class TrafficNetworkWithRoot
    {
        #region Private Fields

        private NetworkPointArea[] networkPointAreasField;

        #endregion Private Fields

        #region Public Properties

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("networkPointArea", IsNullable = false)]
        public NetworkPointArea[] networkPointAreas
        {
            get
            {
                return this.networkPointAreasField;
            }
            set
            {
                this.networkPointAreasField = value;
            }
        }

        #endregion Public Properties
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.7.3081.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    // [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://intf.mb.ivu.de/")]
    public partial class Validity
    {
        #region Private Fields

        private System.DateTime fromDateField;

        private bool fromDateFieldSpecified;

        private System.DateTime toDateField;

        private bool toDateFieldSpecified;

        private string validityNameField;

        #endregion Private Fields

        #region Public Properties

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime fromDate
        {
            get
            {
                return this.fromDateField;
            }
            set
            {
                this.fromDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool fromDateSpecified
        {
            get
            {
                return this.fromDateFieldSpecified;
            }
            set
            {
                this.fromDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime toDate
        {
            get
            {
                return this.toDateField;
            }
            set
            {
                this.toDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool toDateSpecified
        {
            get
            {
                return this.toDateFieldSpecified;
            }
            set
            {
                this.toDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string validityName
        {
            get
            {
                return this.validityNameField;
            }
            set
            {
                this.validityNameField = value;
            }
        }

        #endregion Public Properties
    }
}