﻿namespace abtestreal.VM
{
    public class ListResponse<TItem>
    {
        public TItem[] Items { get; set; }
        public int Count => Items.Length;
    }
}