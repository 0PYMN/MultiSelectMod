using BrutalAPI;
using FMODUnity;
using System;

namespace MultiSelectMod
{
    public static class Class1
    {
        public static void RePickOption(this SelectableCharacterInformationLayout self, int optionID)
        {
            self.IgnoredAbility = optionID;
            if (self.IgnoredAbility < 0 || self.IgnoredAbility >= self._options.Length)
            {
                self.IgnoredAbility = 0;
            }
            if (!(self._character == null) && !self._character.Equals(null))
            {
                bool usesAllAbilities = self._character.usesAllAbilities;
                int rank = self._character.ClampRank(0);
                self.SetAbilities(self._character, rank, usesAllAbilities);
                self.SetOptions(usesAllAbilities);
            }
        }

    }
}
