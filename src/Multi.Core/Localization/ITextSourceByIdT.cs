using System;
using System.Collections.Generic;

namespace Multi.Localization
{
    /// <summary>
    /// Generic interface for text sources (by identifier)
    /// </summary>
    public interface ITextSourceById<Tid>
    {
        /// <summary>
        /// Tries to get the text with the specified ID.
        /// </summary>
        /// <param name="textId">The identifier of the text</param>
        /// <param name="text">The found text</param>
        /// <returns>Returns true if a text with the provided identifier is found, otherwise it returns false.</returns>
        bool TryGetTextById(Tid textId, out string text);

        /// <summary>
        /// Tries to get the texts with the specified Ids.
        /// </summary>
        /// <param name="textIds">The identifiers of the texts</param>
        /// <param name="texts">The found texts</param>
        /// <returns>Returns true if all texts with the provided identifiers are found, otherwise it returns false.</returns>
        bool TryGetTextsByIds(IEnumerable<Tid> textIds, out IEnumerable<string> texts);
    }
}
