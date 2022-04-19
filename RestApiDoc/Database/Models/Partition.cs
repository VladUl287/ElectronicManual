namespace RestApiDoc.Database.Models
{
    public class Partition
    {
        private const string empty = @"{\rtf1\ansi\ansicpg1252\uc1\htmautsp\deff2{\fonttbl{\f0\fcharset0 Times New Roman;}{\f2\fcharset0 Segoe UI;}}{\colortbl\red0\green0\blue0;\red255\green255\blue255;}\loch\hich\dbch\pard\plain\ltrpar\itap0{\lang1033\fs18\f2\cf0 \cf0\ql{\f2 {\ltrch }\li0\ri0\sa0\sb0\fi0\ql\par}{\f2 \li0\ri0\sa0\sb0\fi0\ql\par{\f2 {\ltrch}\li0\ri0\sa0\sb0\fi0\ql\par}}}";

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Text { get; set; } = empty;
        public int ChapterId { get; set; }
        public Chapter Chapter { get; set; }
    }
}