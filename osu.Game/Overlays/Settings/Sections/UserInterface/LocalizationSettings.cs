using System;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Game.Configuration;

namespace osu.Game.Overlays.Settings.Sections.UserInterface
{
    public class LocalizationSettings: SettingsSubsection
    {
        protected override string Header => Properties.strings.LocalizationMenu;

        [BackgroundDependencyLoader]
        private void load(OsuConfigManager config)
        {
            Children = new Drawable[]
            {
                new SettingsDropdown<Locale>
                {
                    LabelText = Properties.strings.Language,
                    Current = config.GetBindable<Locale>(OsuSetting.Locale),
                    Items = Enum.GetValues(typeof(Locale)).Cast<Locale>()
                }
            };
        }
    }
}
