using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelBusters.EasyMLKit
{
    public partial class TextGroup
    {
        public string Text
        {
            get;
            private set;
        }

        public List<Block> Blocks
        {
            get;
            private set;
        }  

        public TextGroup(string text, List<Block> blocks)
        {
            Text = text;
            Blocks = blocks;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}