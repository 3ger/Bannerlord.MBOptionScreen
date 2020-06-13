﻿using MCM.Abstractions;
using MCM.Abstractions.Attributes;
using MCM.Abstractions.Settings.Base;

using Newtonsoft.Json;

using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace MCM.Implementation.Settings.Formats.Xml
{
    [Version("e1.0.0",  1)]
    [Version("e1.0.1",  1)]
    [Version("e1.0.2",  1)]
    [Version("e1.0.3",  1)]
    [Version("e1.0.4",  1)]
    [Version("e1.0.5",  1)]
    [Version("e1.0.6",  1)]
    [Version("e1.0.7",  1)]
    [Version("e1.0.8",  1)]
    [Version("e1.0.9",  1)]
    [Version("e1.0.10", 1)]
    [Version("e1.0.11", 1)]
    [Version("e1.1.0",  1)]
    [Version("e1.2.0",  1)]
    [Version("e1.2.1",  1)]
    [Version("e1.3.0",  1)]
    [Version("e1.3.1",  1)]
    [Version("e1.4.0",  1)]
    [Version("e1.4.1",  1)]
    internal sealed class XmlSettingsFormat : BaseJsonSettingsFormat, IXmlSettingsFormat
    {
        public override IEnumerable<string> Extensions => new[] { "xml" };

        public override bool Save(BaseSettings settings, string path)
        {
            var content = SaveJson(settings);
            var xmlDocument = JsonConvert.DeserializeXmlNode(content, settings is IWrapper wrapper1 ? wrapper1.Object.GetType().Name : settings.GetType().Name);

            var file = new FileInfo(path);
            file.Directory?.Create();
            var writer = file.CreateText();
            xmlDocument.Save(writer);
            writer.Dispose();

            return true;
        }

        public override BaseSettings? Load(BaseSettings settings, string path)
        {
            var file = new FileInfo(path);
            if (file.Exists)
            {
                var xmlDocument = new XmlDocument();
                var reader = file.OpenText();
                xmlDocument.Load(reader);
                reader.Dispose();

                var content = JsonConvert.SerializeXmlNode(xmlDocument);

                var set = LoadFromJson(settings, content);
                if (set == null)
                    Save(settings, path);
            }
            else
            {
                Save(settings, path);
            }
            return settings;
        }
    }
}