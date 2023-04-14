using System;
using System.Collections.Generic;
<<<<<<< Updated upstream
=======
using System.IO;
using System.Reflection;
>>>>>>> Stashed changes

namespace Ink
{
    public class PluginManager
    {
<<<<<<< Updated upstream
        public PluginManager (List<string> pluginNames)
        {
            _plugins = new List<IPlugin> ();

            // TODO: Make these plugin names DLL filenames, and load up their assemblies
            foreach (string pluginName in pluginNames) {
                //if (pluginName == "ChoiceListPlugin") {
                //    _plugins.Add (new InkPlugin.ChoiceListPlugin ());
                //}else  
                {
                    throw new System.Exception ("Plugin not found");
=======
        public PluginManager (List<string> pluginDirectories)
        {
            _plugins = new List<IPlugin> ();

            foreach (string pluginName in pluginDirectories) 
            {
                foreach (string file in Directory.GetFiles(pluginName, "*.dll"))
                {
                    foreach (Type type in Assembly.LoadFile(Path.GetFullPath(file)).GetExportedTypes())
                    {
                        if (typeof(IPlugin).IsAssignableFrom(type))
                        {
                            _plugins.Add((IPlugin)Activator.CreateInstance(type));
                        }
                    }
>>>>>>> Stashed changes
                }
            }
        }

<<<<<<< Updated upstream
        public void PostParse(Parsed.Story parsedStory)
        {
            foreach (var plugin in _plugins) {
                plugin.PostParse (parsedStory);
            }
        }

        public void PostExport(Parsed.Story parsedStory, Runtime.Story runtimeStory)
        {
            foreach (var plugin in _plugins) {
                plugin.PostExport (parsedStory, runtimeStory);
            }
=======
		public string PreParse(string storyContent)
		{
			object[] args = new object[] { storyContent };

            foreach (var plugin in _plugins) 
            {
                typeof(IPlugin).InvokeMember("PreParse", BindingFlags.InvokeMethod, null, plugin, args);
            }

			return (string)args[0];
		}

        public Parsed.Story PostParse(Parsed.Story parsedStory)
        {
            object[] args = new object[] { parsedStory };

            foreach (var plugin in _plugins) 
            {
                typeof(IPlugin).InvokeMember("PostParse", BindingFlags.InvokeMethod, null, plugin, args);
            }

			return (Parsed.Story)args[0];
        }

        public Runtime.Story PostExport(Parsed.Story parsedStory, Runtime.Story runtimeStory)
        {
            object[] args = new object[] { parsedStory, runtimeStory };

            foreach (var plugin in _plugins) 
            {
                typeof(IPlugin).InvokeMember("PostExport", BindingFlags.InvokeMethod, null, plugin, args);
            }

			return (Runtime.Story)args[1];
>>>>>>> Stashed changes
        }

        List<IPlugin> _plugins;
    }
}

