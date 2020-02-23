# SFML.NET Zipped Assets

This project illustrates how to extract assets from an archive file, translate them into usable SFML objects, and then use them in certain aspects throughout a demo program.

The original idea for this came from a demo I wrote with SFML and PhysicsFS some time ago that accomplished something similar. This demo uses SFML.NET and System.IO.Compression to achieve the same thing, however there are some differences. Some of what follows is based on observational evidence and is not yet backed up with anything scientific-like.

It would appear that System.IO.Compression.ZipArchiveEntry treates files/directories within a Zip Archive differently than what PhysicsFS did in that there's virtually no distinction between a file and a directory. ZAE appears to treat each entry as a raw Stream, and looser, albeit potentially inadequate, means can be used to ascertain the preferred existential mode of a given entry (which is what happens in this example code).

The current version 
