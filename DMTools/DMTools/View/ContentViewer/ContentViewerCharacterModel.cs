﻿using DMTools.Keys;
using DMTools.Models.SettingModels;
using DMTools.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace DMTools.View.ContentViewer
{
    class ContentViewerCharacterModel : ContentViewerViewModel
    {
        #region Variables and Properties

        CharacterRepository Repository => CharacterRepository.GetInstance();

        CharacterModel m_model;

        public string WDW_Title { get => $"DM Tools - Character : {m_model.Name}"; }

        #endregion

        #region Constructors

        public ContentViewerCharacterModel(CharacterModel model)
        { m_model = model; }

        #endregion

        #region Functions

        public override FlowDocument GetDocument()
        {
            var result = new FlowDocument();
            AddHeading1(result, $"{m_model.Name}");
            AddHeading2(result, $"{m_model.Concept}");
            if (m_model.Race != "" && m_model.Class != "") AddHeading2(result, $"{m_model.Race} - {m_model.Class}");
            else if (m_model.Race != "") AddHeading2(result, $"{m_model.Race}");
            else if (m_model.Class != "") AddHeading2(result, $"{m_model.Class}");
            AddHeading2(result, $"Notes:");
            AddList(result, m_model.Notes);
            return result;
        }

        public override void Update()
        {
            var model = Repository.GetSectionById(m_model.ID);
            m_model = model;
            if (model == null) OnPropertyChanged(PropertyEventKeys.Close);
            else OnPropertyChanged(nameof(GetDocument));
        }

        #endregion
    }
}
