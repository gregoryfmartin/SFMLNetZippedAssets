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
        private Dictionary<String, Texture> libraryTextures;

        public Dictionary<String, Texture> LibraryTextures { get { return libraryTextures; } }

        public AssetManager () {
            libraryTextures = new Dictionary<String, Texture> ();
        }

        public async void LoadAssets (String archive) {
            ZipArchive zipArchive = null;

            try {
                zipArchive = await OpenZipFile (archive);

                foreach (ZipArchiveEntry entry in zipArchive.Entries) {
                    String [] filename = entry.Name.Split (new char [] { '.' });
                    if (filename.Length > 1) {
                        // This is a file, not a directory
                        if (filename [1].Equals ("png") || filename [1].Equals ("jpg") || filename [1].Equals ("jpeg")) {
                            //byte [] b;
                            //using (MemoryStream ms = new MemoryStream ()) {
                            //    entry.Open ().CopyTo (ms);
                            //    b = ms.ToArray ();
                            //}
                            ////libraryTextures.Add (new Texture (b));
                            //libraryTextures.Add (filename [0], new Texture (b));
                            libraryTextures.Add (filename [0], (await CopyTextureMem (entry)));
                        }
                    }
                }
            } catch (Exception e) {
                Console.WriteLine (e.Message);
                Environment.Exit (-99);
            }
        }

        private async Task<ZipArchive> OpenZipFile (String archive) {
            return ZipFile.OpenRead (archive);
        }

        private async Task<Texture> CopyTextureMem (ZipArchiveEntry entry) {
            byte [] b;
            using (MemoryStream ms = new MemoryStream ()) {
                entry.Open ().CopyTo (ms);
                b = ms.ToArray ();
            }

            return new Texture (b);
        }
    }
}
