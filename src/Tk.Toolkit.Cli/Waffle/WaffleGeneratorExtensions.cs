using System.Text;

namespace Tk.Toolkit.Cli.Waffle
{
    internal static class WaffleGeneratorExtensions
    {
        public static string TitleCaseWords(this string input) => System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input);

        public static StringBuilder EvaluatePhrase(this StringBuilder accum, GenContext ctx, string phrase)
        {
            for (int i = 0; i < phrase.Length; i++)
            {
                if (phrase[i] == '|' && i + 1 < phrase.Length)
                {
                    i++;

                    var escape = accum;
                    var titleCase = false;

                    if (phrase[i] == 'u' && i + 1 < phrase.Length)
                    {
                        escape = new StringBuilder();
                        titleCase = true;
                        i++;
                    }

                    switch (phrase[i])
                    {
                        case 'b':
                            escape.EvaluateOrdinalSequence(ctx);
                            break;
                        case 'c':
                            escape.EvaluateRandomPhrase(ctx, ctx.Phrases.GetPhrases(PhraseKind.Buzzphrase));
                            break;
                        case 'd':
                            escape.EvaluateRandomPhrase(ctx, ctx.Phrases.GetPhrases(PhraseKind.Verb));
                            break;
                        case 'e':
                            escape.EvaluateRandomPhrase(ctx, ctx.Phrases.GetPhrases(PhraseKind.Adverb));
                            break;
                        case 'f':
                            escape.EvaluateRandomPhrase(ctx, ctx.Phrases.GetPhrases(PhraseKind.FirstName));
                            break;
                        case 's':
                            escape.EvaluateRandomPhrase(ctx, ctx.Phrases.GetPhrases(PhraseKind.Surname));
                            break;
                        case 'o':
                            escape.EvaluateRandomPhrase(ctx, ctx.Phrases.GetPhrases(PhraseKind.ArtyNoun));
                            break;
                        case 'y':
                            escape.AppendRandomYear(ctx);
                            break;
                        case 'h':
                            escape.EvaluateRandomPhrase(ctx, ctx.Phrases.GetPhrases(PhraseKind.Prefix));
                            break;
                        case 'A':
                            escape.EvaluateRandomPhrase(ctx, ctx.Phrases.GetPhrases(PhraseKind.Preamble));
                            break;
                        case 'B':
                            escape.EvaluateRandomPhrase(ctx, ctx.Phrases.GetPhrases(PhraseKind.Subject));
                            break;
                        case 'C':
                            escape.EvaluateRandomPhrase(ctx, ctx.Phrases.GetPhrases(PhraseKind.VerbPhrase));
                            break;
                        case 'D':
                            escape.EvaluateRandomPhrase(ctx, ctx.Phrases.GetPhrases(PhraseKind.Object));
                            break;
                        case '1':
                            escape.EvaluateRandomPhrase(ctx, ctx.Phrases.GetPhrases(PhraseKind.FirstAdjective));
                            break;
                        case '2':
                            escape.EvaluateRandomPhrase(ctx, ctx.Phrases.GetPhrases(PhraseKind.SecondAdjective));
                            break;
                        case '3':
                            escape.EvaluateRandomPhrase(ctx, ctx.Phrases.GetPhrases(PhraseKind.Noun));
                            break;
                        case '4':
                            escape.EvaluateRandomPhrase(ctx, ctx.Phrases.GetPhrases(PhraseKind.Cliche));
                            break;
                        case 't':
                            escape.Append(ctx.Title);
                            break;
                        case 'n':
                            escape.Append("[p]".Render(ctx.Rendering));
                            break;
                        case 'p':
                            escape.EvaluateRandomPhrase(ctx, ctx.Phrases.GetPhrases(PhraseKind.Publication));
                            break;
                    }

                    if (titleCase)
                    {
                        accum.Append(escape.ToString().TitleCaseWords().Render(ctx.Rendering));
                    }
                }
                else
                {
                    accum.Append(phrase[i]);
                }
            }
            return accum;
        }

        public static StringBuilder EvaluateRandomPhrase(this StringBuilder output, GenContext ctx, string[] phrases)
        {
            var phrase = ctx.Rng.Pick(phrases);

            return output.EvaluatePhrase(ctx, phrase.Render(ctx.Rendering));
        }

        public static StringBuilder EvaluateOrdinalSequence(this StringBuilder output, GenContext ctx)
        {
            if (ctx.OrdinalSequence >= ctx.Phrases.GetPhrases(PhraseKind.OrdinalSequence).Length)
            {
                ctx.OrdinalSequence = 0;
            }

            output.Append(ctx.Phrases.GetPhrases(PhraseKind.OrdinalSequence)[ctx.OrdinalSequence++]);

            return output;
        }

        public static StringBuilder AppendRandomYear(this StringBuilder output, GenContext ctx)
        {
            output.Append(ctx.Rng.PickDate().Year);
            return output;
        }
    }
}
