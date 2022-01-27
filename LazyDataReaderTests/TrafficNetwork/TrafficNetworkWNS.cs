namespace TrafficNetworkWNS
{
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://intf.mb.ivu.de/")]
    public partial class NetworkPointArea
    {
        #region Private Fields

        private string descriptionField;
        private NetworkPointAreaKey networkPointAreaKeyField;
        private Validity validityField;

        #endregion Private Fields

        #region Public Properties

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

        [System.Xml.Serialization.XmlElement("validity", Namespace = "http://test.de/")]
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

    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://intf.mb.ivu.de/")]
    public partial class NetworkPointAreaKey
    {
        #region Private Fields

        private string abbreviationField;
        private string abbreviationField1;
        private string externalNumberField;
        private string externalNumberField1;
        private string typeField;
        private string typeField1;

        #endregion Private Fields

        #region Public Properties

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

        [System.Xml.Serialization.XmlAttribute(Namespace = "http://intf.mb.ivu.de/")]
        public string abbreviation1
        {
            get
            {
                return this.abbreviationField1;
            }
            set
            {
                this.abbreviationField1 = value;
            }
        }

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

        [System.Xml.Serialization.XmlAttribute(Namespace = "http://intf.mb.ivu.de/")]
        public string externalNumber1
        {
            get
            {
                return this.externalNumberField1;
            }
            set
            {
                this.externalNumberField1 = value;
            }
        }

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

        [System.Xml.Serialization.XmlAttribute(Namespace = "http://test.de/")]
        public string type1
        {
            get
            {
                return this.typeField1;
            }
            set
            {
                this.typeField1 = value;
            }
        }

        #endregion Public Properties
    }

    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlRootAttribute("trafficNetwork", Namespace = "http://intf.mb.ivu.de/", IsNullable = false)]
    public partial class TrafficNetwork
    {
        #region Private Fields

        private NetworkPointArea[] networkPointAreasField;

        #endregion Private Fields

        #region Public Properties

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

    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://test.de/")]
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

        [System.Xml.Serialization.XmlElementAttribute(DataType = "date", Namespace = "http://test.de/")]
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

        [System.Xml.Serialization.XmlElementAttribute(DataType = "date", Namespace = "http://test.de/")]
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