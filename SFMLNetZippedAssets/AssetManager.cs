using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using SFML.Graphics;

namespace SFMLNetZippedAssets {
    sealed class AssetManager {
        private List<Texture> libraryTextures;

        public List<Texture> LibraryTextures { get { return libraryTextures; } }

        public AssetManager () {
            libraryTextures = new List<Texture> ();
        }

        public void LoadAssets (String archive) {
            ZipArchive zipArchive = null;

            try {
                zipArchive = ZipFile.OpenRead (archive);

                foreach (ZipArchiveEntry entry in zipArchive.Entries) {
                    String [] filename = entry.Name.Split (new char [] { '.' });
                    if (filename.Length > 1) {
                        // This is a file, not a directory
                        if (filename [1].Equals ("png") || filename [1].Equals ("jpg") || filename [1].Equals ("jpeg")) {
                            byte [] b;
                            using (MemoryStream ms = new MemoryStream ()) {
                                entry.Open ().CopyTo (ms);
                                b = ms.ToArray ();
                            }
                            libraryTextures.Add (new Texture (b));
                        }
                    }
                }
            } catch (Exception e) {
                Console.WriteLine (e.Message);
                Environment.Exit (-99);
            }
        }
    }
}
