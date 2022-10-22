using System.Text;

namespace Tk.Toolkit.Cli.Waffle
{
    internal static class WaffleEvaluators
    {
        public static string TitleCaseWords(this string input) => System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input);

        public static StringBuilder EvaluatePhrase(this StringBuilder accum, GenContext ctx, string phrase)
        {
            for (int i = 0; i < phrase.Length; i++)
            {
                if (phrase[i] == '|' && i + 1 < phrase.Length)
                {
                    i++;

                    StringBuilder escape = accum;
                    bool titleCase = false;

                    if (phrase[i] == 'u' && i + 1 < phrase.Length)
                    {
                        escape = new StringBuilder();
                        titleCase = true;
                        i++;
                    }

                    switch (phrase[i])
                    {
                        case 'a':
                            escape.EvaluateCardinalSequence(ctx);
                            break;
                        case 'b':
                            escape.EvaluateOrdinalSequence(ctx);
                            break;
                        case 'c':
                            escape.EvaluateRandomPhrase(ctx, Waffles.Buzzphrases);
                            break;
                        case 'd':
                            escape.EvaluateRandomPhrase(ctx, Waffles.Verbs);
                            break;
                        case 'e':
                            escape.EvaluateRandomPhrase(ctx, Waffles.Adverbs);
                            break;
                        case 'f':
                            escape.EvaluateRandomPhrase(ctx, Waffles.Forenames);
                            break;
                        case 's':
                            escape.EvaluateRandomPhrase(ctx, Waffles.Surnames);
                            break;
                        case 'o':
                            escape.EvaluateRandomPhrase(ctx, Waffles.ArtyNouns);
                            break;
                        case 'y':
                            escape.AppendRandomYear(ctx);
                            break;
                        case 'h':
                            escape.EvaluateRandomPhrase(ctx, Waffles.Prefixes);
                            break;
                        case 'A':
                            escape.EvaluateRandomPhrase(ctx, Waffles.PreamblePhrases);
                            break;
                        case 'B':
                            escape.EvaluateRandomPhrase(ctx, Waffles.SubjectPhrases);
                            break;
                        case 'C':
                            escape.EvaluateRandomPhrase(ctx, Waffles.VerbPhrases);
                            break;
                        case 'D':
                            escape.EvaluateRandomPhrase(ctx, Waffles.ObjectPhrases);
                            break;
                        case '1':
                            escape.EvaluateRandomPhrase(ctx, Waffles.FirstAdjectivePhrases);
                            break;
                        case '2':
                            escape.EvaluateRandomPhrase(ctx, Waffles.SecondAdjectivePhrases);
                            break;
                        case '3':
                            escape.EvaluateRandomPhrase(ctx, Waffles.NounPhrases);
                            break;
                        case '4':
                            escape.EvaluateRandomPhrase(ctx, Waffles.Cliches);
                            break;
                        case 't':
                            escape.Append(ctx.Title);
                            break;
                        case 'n':
                            escape.Append("</p>\n<p>");
                            break;
                    }

                    if (titleCase)
                    {
                        accum.Append(escape.ToString().TitleCaseWords());
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

            return output.EvaluatePhrase(ctx, phrase);
        }

        public static StringBuilder EvaluateCardinalSequence(this StringBuilder output, GenContext ctx)
        {
            if (ctx.CardinalSequence >= Waffles.CardinalSequence.Length)
            {
                ctx.CardinalSequence = 0;
            }

            output.Append(Waffles.CardinalSequence[ctx.CardinalSequence++]);

            return output;
        }

        public static StringBuilder EvaluateOrdinalSequence(this StringBuilder output, GenContext ctx)
        {
            if (ctx.OrdinalSequence >= Waffles.OrdinalSequences.Length)
            {
                ctx.OrdinalSequence = 0;
            }

            output.Append(Waffles.OrdinalSequences[ctx.OrdinalSequence++]);

            return output;
        }

        public static StringBuilder AppendRandomYear(this StringBuilder output, GenContext ctx)
        {
            output.Append(ctx.Rng.PickDate().Year);
            return output;
        }
    }
}
