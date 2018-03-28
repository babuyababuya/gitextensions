using System.Text.RegularExpressions;
        public static byte[] GetResetUnstagedLinesAsPatch(GitModule module, string text, int selectionPosition, int selectionLength, Encoding fileContentEncoding)
            ChunkList selectedChunks = ChunkList.GetSelectedChunks(text, selectionPosition, selectionLength, out var header);
            {
            }
            {
            }
            {
            }
            {
            }
        public static byte[] GetSelectedLinesAsPatch(string text, int selectionPosition, int selectionLength, bool staged, Encoding fileContentEncoding, bool isNewFile)
            ChunkList selectedChunks = ChunkList.GetSelectedChunks(text, selectionPosition, selectionLength, out var header);
            {
            }
            {
            }
            {
            }

            string[] headerLines = header.Split(new[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            {
                {
                }
            }
                {
                }
                {
                }
        public static byte[] GetSelectedLinesAsNewPatch(GitModule module, string newFileName, string text, int selectionPosition, int selectionLength, Encoding fileContentEncoding, bool reset, byte[] filePreabmle)
            const string fileMode = "100000"; // given fake mode to satisfy patch format, git will override this

            {
            }
            {
            }

            ChunkList selectedChunks = ChunkList.FromNewFile(module, text, selectionPosition, selectionLength, reset, filePreabmle, fileContentEncoding);
            {
            }

            {
            }
            {
            }
            {
            }
            var s = new StringBuilder();

            byte[] bs = Encoding.UTF8.GetBytes(input);
        public void LoadPatch(string text, Encoding filesContentEncoding)
        public void LoadPatchFile(string path, Encoding filesContentEncoding)
            var text = File.ReadAllText(path, GitModule.LosslessEncoding);
            LoadPatch(text, filesContentEncoding);
    internal sealed class PatchLine
        public string Text { get; private set; }
        public PatchLine(string text, bool selected = false)
        {
            Text = text;
            Selected = selected;
        }

            return new PatchLine(Text, Selected);
    internal sealed class SubChunk
        public List<PatchLine> PreContext { get; } = new List<PatchLine>();
        public List<PatchLine> RemovedLines { get; } = new List<PatchLine>();
        public List<PatchLine> AddedLines { get; } = new List<PatchLine>();
        public List<PatchLine> PostContext { get; } = new List<PatchLine>();
        public string WasNoNewLineAtTheEnd { get; set; }
        public string IsNoNewLineAtTheEnd { get; set; }
            {
            }
                    {
                    }
                    {
                    }

                    {
                    }
                    {
                    }

            {
            }

            {
            }

            {
            }

            {
            }
            {
            }
                    {
                    }
                    {
                    }

            {
            }

            {
            }

            {
            }
            {
            }
    internal sealed class Chunk
        private int _startLine;
        private readonly List<SubChunk> _subChunks = new List<SubChunk>();
        private SubChunk _currentSubChunk;
        private SubChunk CurrentSubChunk
                if (_currentSubChunk == null)
                    _currentSubChunk = new SubChunk();
                    _subChunks.Add(_currentSubChunk);

                return _currentSubChunk;
        private void AddContextLine(PatchLine line, bool preContext)
            {
            }
            {
            }
        private void AddDiffLine(PatchLine line, bool removed)
            {
                _currentSubChunk = null; // start new SubChunk
            }
            {
            }
            {
            }
        /// <summary>
        /// Parses a header line, setting the start index.
        /// </summary>
        /// <remarks>
        /// An example header line is:
        /// <code>
        ///  -116,12 +117,15 @@ private string LoadFile(string fileName, Encoding filesContentEncoding)
        /// </code>
        /// In which case the start line is <c>116</c>.
        /// </remarks>
        private void ParseHeader(string header)
            var match = Regex.Match(header, @".*-(\d+),");
            if (match.Success)
            {
                _startLine = int.Parse(match.Groups[1].Value);
            }
            {
            }
                    var patchLine = new PatchLine(line);

                    {
                    }
                    {
                    }
                        {
                        }
                        {
                        }
                    {
                    }
        public static Chunk FromNewFile(GitModule module, string fileText, int selectionPosition, int selectionLength, bool reset, byte[] filePreabmle, Encoding fileContentEncoding)
            Chunk result = new Chunk { _startLine = 0 };

            if (gitEol == "crlf")
            {
            }
            else if (gitEol == "native")
            {
            }
            {
            }
            string[] lines = fileText.Split(new[] { eol }, StringSplitOptions.None);
                string preamble = i == 0 ? new string(fileContentEncoding.GetChars(filePreabmle)) : string.Empty;
                var patchLine = new PatchLine((reset ? "-" : "+") + preamble + line);

                // do not refactor, there are no breakpoints condition in VS Express
                {
                }
                    if (line != string.Empty)
                            PatchLine lastNotSelectedLine = result.CurrentSubChunk.RemovedLines.LastOrDefault(l => !l.Selected);

                {
                }

            foreach (SubChunk subChunk in _subChunks)
            {
            }
            diff = "@@ -" + _startLine + "," + removedCount + " +" + _startLine + "," + addedCount + " @@".Combine("\n", diff);
        public static ChunkList GetSelectedChunks(string text, int selectionPosition, int selectionLength, out string header)

            {
            }
            {
            }
            string[] chunks = diff.Split(new[] { "\n@@" }, StringSplitOptions.RemoveEmptyEntries);

                    {
                    }

        public static ChunkList FromNewFile(GitModule module, string text, int selectionPosition, int selectionLength, bool reset, byte[] filePreabmle, Encoding fileContentEncoding)
            return new ChunkList
            {
                Chunk.FromNewFile(
                    module,
                    text,
                    selectionPosition,
                    selectionLength,
                    reset,
                    filePreabmle,
                    fileContentEncoding)
            };
        private string ToPatch(SubChunkToPatchFnc subChunkToPatch)
            {
            }
