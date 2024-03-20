// Decompiled with JetBrains decompiler
// Type: MultiSelectMod.Hooks
// Assembly: MultiSelectMod, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2CD6A45B-178B-4FBB-9F40-924F21DA07D1
// Assembly location: C:\Users\windows\Downloads\MultiSelectMod.dll

using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace MultiSelectMod
{
    public static class Hooks
    {
        public static bool firstCharPick = true;
        public static bool secondCharPick = false;
        public static bool mainCharPick = false;
        public static bool defaultNowak = true;
        public static bool defaultSecondChar = true;
        public static int secondID = -1;
        public static int mainID = -1;
        public static SelectableCharacterLayout _secondPickCharLayout;
        public static int firstIgnored = 0;
        public static int secondIgnored = 0;
        public static int mainIgnored = 0;
        public static SelectableMainCharacterData mainCharData = new SelectableMainCharacterData();
        public static SelectableMainCharacterData nowakData = new SelectableMainCharacterData();

        public static void PrepareCharacters(
          Action<SelectableCharactersSO, HashSet<string>> orig,
          SelectableCharactersSO self,
          HashSet<string> unlockedCharacters)
        {
            mainCharData._character = nowakData.Character;
            mainCharData._portrait = nowakData.Portrait;
            mainCharData._noPortrait = nowakData.NoPortrait;
            mainCharData._ignoredAbility = nowakData.IgnoredAbility;
            defaultNowak = true;
            bool flag1 = true;
            defaultSecondChar = true;
            SelectableCharacterData selectableCharacterData = new SelectableCharacterData("Nowak_CH", ResourceLoader.LoadSprite("NowakMenu"), ResourceLoader.LoadSprite("NowakMenu"));
            foreach (SelectableCharacterData character in self._characters)
            {
                if (character.CharacterName == "Nowak_CH")
                    flag1 = false;
            }
            if (flag1)
            {
                List<SelectableCharacterData> selectableCharacterDataList = new List<SelectableCharacterData>();
                selectableCharacterDataList.Add(selectableCharacterData);
                if (!self._dpsCharacters.ContainsKey(new CharacterRefString("Nowak_CH")))
                    self._dpsCharacters.Add(new CharacterRefString("Nowak_CH"), new CharacterIgnoredAbilities()
                    {
                        ignoredAbilities = new List<int>()
                    });
                foreach (SelectableCharacterData character in self._characters)
                    selectableCharacterDataList.Add(character);
                self._characters = selectableCharacterDataList.ToArray();
                foreach (SelectableCharacterData character in self._characters)
                {
                    if (!(character._characterName == "Nowak_CH"))
                        character.TryLoadIfAvailable(unlockedCharacters);
                    else
                        character.LoadedCharacter = LoadedAssetsHandler.GetCharcater(character._characterName);
                    if (character.HasCharacter && !(character._characterName == "Nowak_CH"))
                    {
                        bool flag2 = self._steamAchievements.IsAchievementOfflineUnlocked(character.TheWitnessAch);
                        bool flag3 = self._steamAchievements.IsAchievementOfflineUnlocked(character.TheDivineAch);
                        character.SetAchievementState(flag2, flag3);
                    }
                    else if (character.HasCharacter && character._characterName == "Nowak_CH")
                        character.SetAchievementState(false, false);
                }
            }
            else
                orig(self, unlockedCharacters);
        }

        public static void OnMainCharacterSelected(
          Action<CharacterSelectionHandler> orig,
          CharacterSelectionHandler self)
        {
            firstCharPick = false;
            secondCharPick = false;
            mainCharPick = true;
            if (defaultNowak)
            {
                orig(self);
            }
            else
            {
                RuntimeManager.PlayOneShot(self._characterClickEvent, new Vector3());
                if (self.SelectedID < 0 || self.SelectedID >= self._selectableCharactersData.Length)
                {
                    self._noCharacterInformation.SetActive(true);
                    self._extraCompanionInformation.SetActive(false);
                    self._selectionBiasInformation.SetActive(false);
                    self._trackerInfo.Activation(false);
                }
                else
                {
                    self._noCharacterInformation.SetActive(false);
                    self._extraCompanionInformation.SetActive(false);
                    self._selectionBiasInformation.SetActive(false);
                    self._trackerInfo.Activation(false);
                    self._charInfo.SetInformation(self._selectableCharactersData[mainID].LoadedCharacter);
                }
            }
        }

        public static void OnThirdCharacterSelected(
          Action<CharacterSelectionHandler> orig,
          CharacterSelectionHandler self)
        {
            firstCharPick = false;
            secondCharPick = true;
            mainCharPick = false;
            if (defaultSecondChar)
            {
                orig(self);
            }
            else
            {
                RuntimeManager.PlayOneShot(self._characterClickEvent, new Vector3());
                if (self.SelectedID < 0 || self.SelectedID >= self._selectableCharactersData.Length)
                {
                    self._noCharacterInformation.SetActive(true);
                    self._extraCompanionInformation.SetActive(false);
                    self._selectionBiasInformation.SetActive(false);
                    self._trackerInfo.Activation(false);
                }
                else
                {
                    self._noCharacterInformation.SetActive(false);
                    self._extraCompanionInformation.SetActive(false);
                    self._selectionBiasInformation.SetActive(false);
                    self._trackerInfo.Activation(false);
                    self._charInfo.SetInformation(self._selectableCharactersData[secondID].LoadedCharacter);
                }
            }
        }

        public static void OnCompanionCharacterSelected(
          Action<CharacterSelectionHandler> orig,
          CharacterSelectionHandler self)
        {
            firstCharPick = true;
            secondCharPick = false;
            mainCharPick = false;
            orig(self);
        }

        public static void OnRandomCharacterSelected(
          Action<CharacterSelectionHandler> orig,
          CharacterSelectionHandler self)
        {
            RuntimeManager.PlayOneShot(self._characterClickEvent, new Vector3());
            if (firstCharPick)
            {
                if (self.SelectedID != secondID && self.SelectedID != mainID)
                    self.DeselectCharacter(self.SelectedID);
            }
            else if (secondCharPick)
            {
                defaultSecondChar = false;
                if (self.SelectedID != secondID && secondID != mainID)
                    self.DeselectCharacter(secondID);
            }
            else if (mainCharPick)
            {
                defaultNowak = false;
                if (self.SelectedID != mainID && secondID != mainID)
                    self.DeselectCharacter(mainID);
            }
            self._selRandomCharLayout.CharacterSelection(true);
            self._noCharacterInformation.SetActive(true);
            self._extraCompanionInformation.SetActive(false);
            self._selectionBiasInformation.SetActive(false);
            self._trackerInfo.Activation(false);
            if (firstCharPick)
            {
                self._selCompanionCharLayout.SetInformation(self._randomCharImage, self._randomCharImage, true, false, false, false);
                self.SelectedID = -1;
                firstIgnored = -1;
            }
            else if (secondCharPick)
            {
                _secondPickCharLayout.SetInformation(self._randomCharImage, self._randomCharImage, true, false, false, false);
                secondID = -1;
                secondIgnored = -1;
            }
            else
            {
                if (!mainCharPick)
                    return;
                self._selMainCharLayout.SetInformation(self._randomCharImage, self._randomCharImage, true, false, false, false);
                mainID = -1;
                mainIgnored = -1;
            }
        }

        public static void OnCharacterSelected(
          Action<CharacterSelectionHandler, int> orig,
          CharacterSelectionHandler self,
          int id)
        {
            if (id < 0 || id >= self._selectableCharactersData.Length)
                return;
            SelectableCharacterData selectableCharacterData = self._selectableCharactersData[id];
            if (selectableCharacterData.HasCharacter)
            {
                RuntimeManager.PlayOneShot(self._characterClickEvent, new Vector3());
                if (firstCharPick)
                {
                    if (self.SelectedID != secondID && self.SelectedID != mainID)
                        self.DeselectCharacter(self.SelectedID);
                    self.SelectedID = id;
                    firstIgnored = 0;
                }
                else if (secondCharPick)
                {
                    defaultSecondChar = false;
                    if (self.SelectedID != secondID && secondID != mainID)
                        self.DeselectCharacter(secondID);
                    secondID = id;
                    secondIgnored = 0;
                }
                else if (mainCharPick)
                {
                    defaultNowak = false;
                    if (self.SelectedID != mainID && secondID != mainID)
                        self.DeselectCharacter(mainID);
                    mainID = id;
                    mainIgnored = 0;
                }
                self._selectableCharactersUI[id].CharacterSelection(true);
                self._noCharacterInformation.SetActive(false);
                self._extraCompanionInformation.SetActive(false);
                self._selectionBiasInformation.SetActive(false);
                self._trackerInfo.Activation(false);
                self._charInfo.SetInformation(selectableCharacterData.LoadedCharacter);
                if (firstCharPick)
                    self._selCompanionCharLayout.SetInformation(selectableCharacterData.Portrait, selectableCharacterData.NoPortrait, true, false, false, false);
                else if (secondCharPick)
                {
                    _secondPickCharLayout.SetInformation(selectableCharacterData.Portrait, selectableCharacterData.NoPortrait, true, false, false, false);
                }
                else
                {
                    if (!mainCharPick)
                        return;
                    self._selMainCharLayout.SetInformation(selectableCharacterData.Portrait, selectableCharacterData.NoPortrait, true, false, false, false);
                    mainCharData._portrait = selectableCharacterData.Portrait;
                    mainCharData._noPortrait = selectableCharacterData.NoPortrait;
                }
            }
            else
            {
                RuntimeManager.PlayOneShot(self._characterClickEvent, new Vector3());
                bool flag = true;
                if (selectableCharacterData.TrackData != null)
                    flag = !selectableCharacterData.TrackData.IsTrackDataAvailable(self._infoHolder);
                if (flag)
                    self._trackerInfo.SetLockedInfo(selectableCharacterData.NoPortrait);
                else if (selectableCharacterData.TrackData.UsesSlider)
                    self._trackerInfo.SetSliderInformation(selectableCharacterData.Portrait, selectableCharacterData.TrackData.GetDescription(self._infoHolder), selectableCharacterData.TrackData.GetSliderValue(self._infoHolder));
                else
                    self._trackerInfo.SetInformation(selectableCharacterData.Portrait, selectableCharacterData.TrackData.GetDescription(self._infoHolder));
                self._trackerInfo.Activation(true);
                self._noCharacterInformation.SetActive(false);
                self._extraCompanionInformation.SetActive(false);
                self._selectionBiasInformation.SetActive(false);
                Debug.Log((object)"empty character?");
            }
        }

        public static void OnOptionSelected(
          Action<SelectableCharacterInformationLayout, int> orig,
          SelectableCharacterInformationLayout self,
          int optionID)
        {
            orig(self, optionID);
            if (firstCharPick)
                firstIgnored = optionID;
            else if (secondCharPick)
            {
                secondIgnored = optionID;
            }
            else
            {
                if (!mainCharPick)
                    return;
                mainIgnored = optionID;
                mainCharData._ignoredAbility = optionID;
            }
        }

        public static void Activation(
          Action<CharacterSelectionHandler, bool> orig,
          CharacterSelectionHandler self,
          bool enabled)
        {
            orig(self, enabled);
            foreach (SelectableCharacterLayout selectableCharacterLayout in UnityEngine.Object.FindObjectsOfType(typeof(SelectableCharacterLayout)))
            {
                if ((selectableCharacterLayout).name == "RandomCompanionLayout")
                    _secondPickCharLayout = selectableCharacterLayout;
            }
        }

        public static void Start(Action<MainMenuController> orig, MainMenuController self)
        {
            orig(self);
            mainCharData = new SelectableMainCharacterData();
            nowakData = self._charSelectionDB.MainCharacter;
        }

        public static IEnumerator PrepareNewRunData(
          Func<MainMenuController, bool, IEnumerator> orig,
          MainMenuController self,
          bool useCheater = false)
        {
            RunDataSO runDataSO = ScriptableObject.CreateInstance<RunDataSO>();
            List<InitialCharacter> initialCharacterList = new List<InitialCharacter>();
            bool selectionBiasActivity = self._charSelection.SelectionBiasActivity;
            if (selectionBiasActivity != self._saveDataHandler.SelectionBiasActive)
                SaveManager.UpdateSelectionBiasSaveData(selectionBiasActivity);
            SelectableCharacterData[] characters = self._charSelectionDB.Characters;
            List<CharacterSO> possibleCharacters = new List<CharacterSO>();
            SelectableCharacterData[] selectableCharacterDataArray = characters;
            for (int index = 0; index < selectableCharacterDataArray.Length; ++index)
            {
                SelectableCharacterData selectableCharacterData = selectableCharacterDataArray[index];
                if (selectableCharacterData.HasCharacter && !selectableCharacterData.IgnoreRandomSelection)
                    possibleCharacters.Add(selectableCharacterData.LoadedCharacter);
                selectableCharacterData = null;
            }
            selectableCharacterDataArray = null;
            SelectableMainCharacterData mainCharacter = self._charSelectionDB.MainCharacter;
            CharacterSO maincharaSO = useCheater ? self._cheaterCharacter : mainCharacter.Character;
            if (mainID < 0 || mainID >= characters.Length || !characters[mainID].HasCharacter)
            {
                int index = UnityEngine.Random.Range(0, possibleCharacters.Count);
                maincharaSO = possibleCharacters[index];
                if (possibleCharacters.Contains(maincharaSO) && !defaultNowak)
                    possibleCharacters.Remove(maincharaSO);
            }
            else
            {
                maincharaSO = characters[mainID].LoadedCharacter;
                if (possibleCharacters.Contains(maincharaSO) && !defaultNowak)
                    possibleCharacters.Remove(maincharaSO);
            }
            if (possibleCharacters.Contains(mainCharacter.Character) && defaultNowak)
                possibleCharacters.Remove(mainCharacter.Character);
            int ignoredAbility1 = mainCharacter.IgnoredAbility;
            if (ignoredAbility1 != mainIgnored)
                ignoredAbility1 = mainIgnored;
            if (ignoredAbility1 < 0 || ignoredAbility1 >= 3)
                ignoredAbility1 = UnityEngine.Random.Range(0, 3);
            if (!defaultNowak)
                initialCharacterList.Add(new InitialCharacter(maincharaSO, 0, ignoredAbility1, true));
            else
                initialCharacterList.Add(new InitialCharacter(nowakData.Character, 0, nowakData.IgnoredAbility, true));
            int selectedID = self._charSelection.SelectedID;
            ignoredAbility1 = firstIgnored;
            if (selectedID < 0 && secondID >= 0)
            {
                selectedID = secondID;
                secondID = -1;
            }
            CharacterSO firstParty;
            if (selectedID < 0 || selectedID >= characters.Length || !characters[selectedID].HasCharacter)
            {
                int index = UnityEngine.Random.Range(0, possibleCharacters.Count);
                firstParty = possibleCharacters[index];
                possibleCharacters.RemoveAt(index);
            }
            else
            {
                firstParty = characters[selectedID].LoadedCharacter;
                possibleCharacters.Remove(firstParty);
            }
            if (ignoredAbility1 < 0 || ignoredAbility1 >= 3)
                ignoredAbility1 = UnityEngine.Random.Range(0, 3);
            if (firstParty != null)
                initialCharacterList.Add(new InitialCharacter(firstParty, 0, ignoredAbility1, false));
            CharacterSO secondParty = null;
            int ignoredAbility2 = 0;
            if (secondID < 0 || secondID >= characters.Length || !characters[secondID].HasCharacter)
            {
                if (selectionBiasActivity)
                    secondParty = self._charSelectionDB.GetCharacterByBias(firstParty, ignoredAbility1, possibleCharacters, out ignoredAbility2);
                if (secondParty == null)
                {
                    ignoredAbility2 = UnityEngine.Random.Range(0, 3);
                    int index = UnityEngine.Random.Range(0, possibleCharacters.Count);
                    secondParty = possibleCharacters[index];
                    if (possibleCharacters.Contains(secondParty))
                        possibleCharacters.Remove(secondParty);
                }
            }
            else
            {
                if (secondID < 0 || secondID >= characters.Length || !characters[secondID].HasCharacter)
                {
                    int index = UnityEngine.Random.Range(0, possibleCharacters.Count);
                    secondParty = possibleCharacters[index];
                    if (possibleCharacters.Contains(secondParty))
                        possibleCharacters.Remove(secondParty);
                }
                else
                {
                    secondParty = characters[Hooks.secondID].LoadedCharacter;
                    if (possibleCharacters.Contains(secondParty))
                        possibleCharacters.Remove(secondParty);
                }
                ignoredAbility2 = secondIgnored;
                if (ignoredAbility2 < 0 || ignoredAbility2 >= 3)
                    ignoredAbility2 = UnityEngine.Random.Range(0, 3);
            }
            if (secondParty != null)
                initialCharacterList.Add(new InitialCharacter(secondParty, 0, ignoredAbility2, false));
            runDataSO.InitializeRun(self._informationHolder.Game, initialCharacterList.ToArray(), self._informationHolder.GetZoneDBs());
            runDataSO.zoneLoadingType = (ZoneLoadingType)0;
            MainTutorialDataSO tutorialData = self._saveDataHandler.TutorialData;
            OverworldTutorialHandler oWTutorialData = self._saveDataHandler.OWTutorialData;
            self._informationHolder.PrepareGameRun(runDataSO, oWTutorialData, tutorialData);
            yield return runDataSO.InitializeDataBase(self._informationHolder.Game, self._informationHolder.ItemPoolDB);
            self.FinalizeMainMenuSounds();
            yield return self.LoadNextScene(self._owSceneToLoad);
        }
    }
}
