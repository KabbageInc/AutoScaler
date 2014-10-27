using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SimpleAWS.Models.EC2
{
    public enum InstanceType
    {
        [XmlEnum(Name = "c1.medium")]
        c1medium,
        [XmlEnum(Name = "c1.xlarge")]
        c1xlarge,
        [XmlEnum(Name = "c3.2xlarge")]
        c32xlarge,
        [XmlEnum(Name = "c3.4xlarge")]
        c34xlarge,
        [XmlEnum(Name = "c3.8xlarge")]
        c38xlarge,
        [XmlEnum(Name = "c3.large")]
        c3large,
        [XmlEnum(Name = "c3.xlarge")]
        c3xlarge,
        [XmlEnum(Name = "cc.14xlarge")]
        cc14xlarge,
        [XmlEnum(Name = "cc.28xlarge")]
        cc28xlarge,
        [XmlEnum(Name = "cg.14xlarge")]
        cg14xlarge,
        [XmlEnum(Name = "cr.18xlarge")]
        cr18xlarge,
        [XmlEnum(Name = "g2.2xlarge")]
        g22xlarge,
        [XmlEnum(Name = "hi.14xlarge")]
        hi14xlarge,
        [XmlEnum(Name = "hs.18xlarge")]
        hs18xlarge,
        [XmlEnum(Name = "i2.2xlarge")]
        i22xlarge,
        [XmlEnum(Name = "i2.4xlarge")]
        i24xlarge,
        [XmlEnum(Name = "i2.8xlarge")]
        i28xlarge,
        [XmlEnum(Name = "i2.xlarge")]
        i2xlarge,
        [XmlEnum(Name = "m1.large")]
        m1large,
        [XmlEnum(Name = "m1.medium")]
        m1medium,
        [XmlEnum(Name = "m1.small")]
        m1small,
        [XmlEnum(Name = "m1.xlarge")]
        m1xlarge,
        [XmlEnum(Name = "m2.2xlarge")]
        m22xlarge,
        [XmlEnum(Name = "m2.4xlarge")]
        m24xlarge,
        [XmlEnum(Name = "m2.xlarge")]
        m2xlarge,
        [XmlEnum(Name = "m3.2xlarge")]
        m32xlarge,
        [XmlEnum(Name = "m3.large")]
        m3large,
        [XmlEnum(Name = "m3.medium")]
        m3medium,
        [XmlEnum(Name = "m3.xlarge")]
        m3xlarge,
        [XmlEnum(Name = "t1.micro")]
        t1micro,
    }
}
