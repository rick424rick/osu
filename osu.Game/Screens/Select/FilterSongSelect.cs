﻿using System;
using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Primitives;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Input;
using osu.Game.Graphics;

namespace osu.Game.Screens.Select
{
    public class FilterSongSelect : Container
    {
        public enum SortMode
        {
            Arist,
            BPM,
            Creator,
            DateAdded,
            Difficulty,
            Length,
            RankAchieved,
            Title
        }

        public enum GroupMode
        {
            NoGrouping,
            Arist,
            BPM,
            Creator,
            DateAdded,
            Difficulty,
            Length,
            RankAchieved,
            Title,
            Collections,
            Favorites,
            MyMaps,
            RankedStatus,
            RecentlyPlayed
        }
    
        public Action FilterChanged;

        public string Search { get; private set; } = string.Empty;
        public SortMode Sort { get; private set; } = SortMode.Title;

        public FilterSongSelect()
        {
            AutoSizeAxes = Axes.Y;

            Children = new Drawable[]
            {
                new Box
                {
                    Colour = Color4.Black,
                    Alpha = 0.6f,
                    RelativeSizeAxes = Axes.Both,
                },
                new FlowContainer
                {
                    Padding = new MarginPadding(20),
                    AutoSizeAxes = Axes.Y,
                    RelativeSizeAxes = Axes.X,
                    Anchor = Anchor.TopRight,
                    Origin = Anchor.TopRight,
                    Width = 0.4f, // TODO: InnerWidth property or something
                    Direction = FlowDirection.VerticalOnly,
                    Children = new Drawable[]
                    {
                        new SearchTextBox { RelativeSizeAxes = Axes.X },
                        new GroupSortTabs()
                    }
                }
            };
        }

        private class GroupSortItem : ClickableContainer
        {
            public string Text
            {
                get { return text.Text; }
                set { text.Text = value; }
            }

            private void FadeActive()
            {
                box.FadeIn(300);
                text.FadeColour(Color4.White, 300);
            }

            private void FadeInactive()
            {
                box.FadeOut(300);
                text.FadeColour(new Color4(102, 204, 255, 255), 300);
            }

            private bool active;
            public bool Active
            {
                get { return active; }
                set
                {
                    active = value;
                    if (active)
                        FadeActive();
                    else
                        FadeInactive();
                }
            }
        
            private SpriteText text;
            private Box box;
            
            protected override bool OnHover(InputState state)
            {
                if (!active)
                    FadeActive();
                return true;
            }
            
            protected override void OnHoverLost(InputState state)
            {
                if (!active)
                    FadeInactive();
            }
        
            public GroupSortItem()
            {
                AutoSizeAxes = Axes.Both;
                Children = new Drawable[]
                {
                    text = new SpriteText
                    {
                        Colour = new Color4(102, 204, 255, 255),
                        Margin = new MarginPadding(5),
                        TextSize = 14,
                        Font = @"Exo2.0-Bold",
                    },
                    box = new Box
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = 1,
                        Alpha = 0,
                        Colour = Color4.White,
                        Origin = Anchor.BottomLeft,
                        Anchor = Anchor.BottomLeft,
                    }
                };
            }
        }

        private class GroupSortTabs : Container
        {
            public GroupSortTabs()
            {
                RelativeSizeAxes = Axes.X;
                AutoSizeAxes = Axes.Y;
                Children = new Drawable[]
                {
                    new Box
                    {
                        RelativeSizeAxes = Axes.X,
                        Height = 1,
                        Colour = new Color4(80, 80, 80, 255),
                        Origin = Anchor.BottomLeft,
                        Anchor = Anchor.BottomLeft,
                    },
                    new FlowContainer
                    {
                        AutoSizeAxes = Axes.Both,
                        Direction = FlowDirection.HorizontalOnly,
                        Spacing = new Vector2(10, 0),
                        Children = new Drawable[]
                        {
                            new GroupSortItem
                            {
                                Text = "All",
                                Active = true,
                            },
                            new GroupSortItem
                            {
                                Text = "Recently Played",
                                Active = false,
                            },
                            new GroupSortItem
                            {
                                Text = "Collections",
                                Active = false,
                            },
                            new TextAwesome
                            {
                                Icon = FontAwesome.fa_ellipsis_h,
                                Colour = new Color4(102, 204, 255, 255),
                                TextSize = 14,
                                Margin = new MarginPadding { Top = 5, Bottom = 5 },
                                Origin = Anchor.BottomLeft,
                                Anchor = Anchor.BottomLeft,
                            }
                        }
                    },
                    new FlowContainer
                    {
                        AutoSizeAxes = Axes.Both,
                        Direction = FlowDirection.HorizontalOnly,
                        Spacing = new Vector2(10, 0),
                        Origin = Anchor.TopRight,
                        Anchor = Anchor.TopRight,
                        Children = new Drawable[]
                        {
                            new SpriteText
                            {
                                Font = @"Exo2.0-Bold",
                                Text = "Sort results by",
                                Colour = new Color4(165, 204, 0, 255),
                                TextSize = 14,
                                Margin = new MarginPadding { Top = 5, Bottom = 5 },
                            },
                            new GroupSortItem
                            {
                                Text = "Artist",
                                Active = true,
                            },
                            new TextAwesome
                            {
                                Icon = FontAwesome.fa_ellipsis_h,
                                Colour = new Color4(165, 204, 0, 255),
                                TextSize = 14,
                                Margin = new MarginPadding { Top = 5, Bottom = 5 },
                                Origin = Anchor.BottomLeft,
                                Anchor = Anchor.BottomLeft,
                            }
                        }
                    },
                };
            }
        }
    }
}