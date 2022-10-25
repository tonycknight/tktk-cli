namespace Tk.Toolkit.Cli.Waffle
{
    public enum PhraseKind
    {
        Preamble,
        Publication,
        Subject,
        VerbPhrase,
        Object,
        Adverb,
        Verb,
        FirstAdjective,
        SecondAdjective,
        Noun,
        Cliche,
        Prefix,
        ArtyNoun,
        Surname,
        FirstName,
        Buzzphrase,
        OrdinalSequence,
        MaybeHeading,
        Maybe
    }

    internal interface IPhraseProvider
    {
        string[] GetPhrase(PhraseKind phrase);
    }
}
