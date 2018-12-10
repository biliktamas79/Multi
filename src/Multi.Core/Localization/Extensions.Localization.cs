using System;

namespace Multi.Localization
{
    /// <summary>
    /// Static class with extension methods for the <see cref="ITextSourceById{Tid}"/> interface
    /// </summary>
    public static class TextSourceByIdExtensions
    {
        /// <summary>
        /// Gets the text with the specified Id or the provided default if not found
        /// </summary>
        /// <param name="textId">The identifier of the text</param>
        /// <param name="def">The default value to return if not found</param>
        /// <returns>Returns the first text with the specified Id if found, otherwise it returns the provided default value.</returns>
        public static string GetTextByIdOrDefault<Tid>(this ITextSourceById<Tid> textSource, Tid textId, string def)
        {
            //if (textSource == null)
            //    throw new ArgumentNullException("textSource");

            string value;
            if (textSource.TryGetTextById(textId, out value))
                return value;
            else
                return def;
        }

        /// <summary>
        /// Gets the text with the specified Id or throws <see cref="TextNotFoundException"/> if not found.
        /// </summary>
        /// <param name="textId">The identifier of the text</param>
        /// <exception cref="TextNotFoundException">Thrown when the text with the specified textId is not found.</exception>
        /// <returns>Returns the first text with the specified Id if found, otherwise it throws an Exception</returns>
        public static string GetTextByIdOrThrow<Tid>(this ITextSourceById<Tid> textSource, Tid textId)
        {
            //if (textSource == null)
            //    throw new ArgumentNullException("textSource");

            string value;
            if (textSource.TryGetTextById(textId, out value))
                return value;
            else
                throw new TextNotFoundException(textId);//textSource.NewTextNotFoundException(textId);
        }

        //public static ArgumentOutOfRangeException NewTextNotFoundException<Tid>(this ITextSourceById<Tid> textSource, Tid textId)
        //{
        //    return new ArgumentOutOfRangeException("textId", string.Format(System.Globalization.CultureInfo.InvariantCulture, "Text with id='{0}' not found!", textId));
        //}
        //public static TextNotFoundException NewTextNotFoundException<Tid>(this ITextSourceById<Tid> textSource, Tid textId)
        //{
        //    return new TextNotFoundException(textId);
        //}
    }
}
