using DMTools.Models.SessionModels;
using DMTools.Models.SettingModels;
using DMTools.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace DMTools.Services
{
    public static class FlowDocumentService
    {
        #region Variables and Properties

        private static CharacterRepository CharRepository => CharacterRepository.GetInstance();

        #endregion

        #region Functions

        public static Inline[] GetCharacterSessionRuns(SessionCharacterModel character)
        {
            var result = new List<Inline>();

            var c = CharRepository.GetObjectById(character.CharacterId);
            if (c == null) return result.ToArray();
            result.Add(new Run($"{c.Name}: ") { FontSize = 14, FontWeight = FontWeights.DemiBold, TextDecorations = TextDecorations.Underline });
            if (c.Race != "" && c.Class != "") result.Add(new Run($"[{c.Race} - {c.Class}] ") { Foreground = Brushes.Gray });
            else if (c.Race != "") result.Add(new Run($"[{c.Race}] ") { Foreground = Brushes.Gray });
            else if (c.Class != "") result.Add(new Run($"[{c.Class}] ") { Foreground = Brushes.Gray });
            result.Add(new Run($"{character.Info}") { });

            return result.ToArray();
        }

        public static Inline[] GetCharacterEventRuns(CharacterEventModel character)
        {
            var result = new List<Inline>();

            var c = CharRepository.GetObjectById(character.CharacterId);
            if (c == null) return result.ToArray();
            result.Add(new Run($"{c.Name}: ") { FontSize = 14, FontWeight = FontWeights.DemiBold, TextDecorations = TextDecorations.Underline });
            if (c.Race != "" && c.Class != "") result.Add(new Run($"[{c.Race} - {c.Class}] ") { Foreground = Brushes.Gray });
            else if (c.Race != "") result.Add(new Run($"[{c.Race}] ") { Foreground = Brushes.Gray });
            else if (c.Class != "") result.Add(new Run($"[{c.Class}] ") { Foreground = Brushes.Gray });
            result.Add(new Run($"{character.Info}") { });

            return result.ToArray();
        }

        #endregion
    }
}
