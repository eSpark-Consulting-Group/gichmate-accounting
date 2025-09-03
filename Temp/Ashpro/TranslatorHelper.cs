using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using AshFramework;
using Ashpro.Properties;

namespace Ashpro
{
	public static class TranslatorHelper
	{
		private static DBContext db = new DBContext();

		private static string sForm;

		public static void Save(Translator translator)
		{
			if (!translator.Id.HasValue)
			{
				AshproCommon.InsertToDatabaseObj(translator);
			}
			else
			{
				AshproCommon.UpdateToDatabaseObj(translator, "Translator", "Id", translator.Id.toInt32());
			}
		}

		public static void Save(List<Translator> translators)
		{
			foreach (Translator translator in translators)
			{
				Save(translator);
			}
		}

		public static void ChangeToArabic(Form form)
		{
			if (!(Settings.Default.Language != "en"))
			{
				return;
			}
			sForm = form.Name;
			foreach (Control control5 in form.Controls)
			{
				if (!(control5 is Label) && !(control5 is Button) && !(control5 is RadioButton) && !(control5 is CheckBox))
				{
					if (control5 is Panel)
					{
						Panel panel = (Panel)control5;
						foreach (Control control6 in panel.Controls)
						{
							if (control6 is Label || control6 is Button || control6 is RadioButton || control6 is CheckBox)
							{
								control6.MouseDoubleClick += Item_MouseDoubleClick;
							}
						}
					}
					else
					{
						if (!(control5 is TabControl))
						{
							continue;
						}
						TabControl tabControl = (TabControl)control5;
						foreach (TabPage tabPage in tabControl.TabPages)
						{
							foreach (Control control7 in tabPage.Controls)
							{
								if (!(control7 is Label) && !(control7 is Button) && !(control7 is RadioButton) && !(control7 is CheckBox))
								{
									if (!(control7 is Panel))
									{
										continue;
									}
									Panel panel2 = (Panel)control7;
									panel2.DoubleClick += Panel_DoubleClick;
									foreach (Control control8 in panel2.Controls)
									{
										if (control8 is Label || control8 is Button || control8 is RadioButton || control8 is CheckBox)
										{
											control8.MouseDoubleClick += Item_MouseDoubleClick;
										}
									}
								}
								else
								{
									control7.MouseDoubleClick += Item_MouseDoubleClick;
								}
							}
						}
					}
				}
				else
				{
					control5.MouseDoubleClick += Item_MouseDoubleClick;
				}
			}
			form.MouseDoubleClick += Form_MouseDoubleClick;
			CultureInfo cultureInfo3 = (Thread.CurrentThread.CurrentCulture = (Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Language)));
		}

		private static void Panel_DoubleClick(object sender, EventArgs e)
		{
			Panel panel = (Panel)sender;
			panel.MouseDoubleClick += Form_MouseDoubleClick;
		}

		private static void Form_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			Form form = new Form();
			if (sender is Panel)
			{
				Panel panel = (Panel)sender;
				form = panel.FindForm();
			}
			else
			{
				form = (Form)sender;
			}
			List<Translator> list = new List<Translator>();
			foreach (Control control5 in form.Controls)
			{
				if (!(control5 is Label) && !(control5 is Button) && !(control5 is RadioButton) && !(control5 is CheckBox))
				{
					if (control5 is Panel)
					{
						Panel panel2 = (Panel)control5;
						foreach (Control control6 in panel2.Controls)
						{
							if (control6 is Label || control6 is Button || control6 is RadioButton || control6 is CheckBox)
							{
								AddToEntityMethod(form, list, control6);
							}
						}
					}
					else
					{
						if (!(control5 is TabControl))
						{
							continue;
						}
						TabControl tabControl = (TabControl)control5;
						foreach (TabPage tabPage in tabControl.TabPages)
						{
							foreach (Control control7 in tabPage.Controls)
							{
								if (!(control7 is Label) && !(control7 is Button) && !(control7 is RadioButton) && !(control7 is CheckBox))
								{
									if (!(control7 is Panel) || !(control7.Name != "pnlProduct"))
									{
										continue;
									}
									Panel panel3 = (Panel)control7;
									foreach (Control control8 in panel3.Controls)
									{
										if (control8 is Label || control8 is Button || control8 is RadioButton || control8 is CheckBox)
										{
											AddToEntityMethod(form, list, control8);
										}
									}
								}
								else
								{
									AddToEntityMethod(form, list, control7);
								}
							}
						}
					}
				}
				else
				{
					AddToEntityMethod(form, list, control5);
				}
			}
			TranslatorInputForm translatorInputForm = new TranslatorInputForm(list);
			if (translatorInputForm.ShowDialog() == DialogResult.OK)
			{
				Save(list);
			}
			LoadArabic(form);
		}

		private static void AddToEntityMethod(Form form, List<Translator> entLanguages, Control item)
		{
			if (item.Text != string.Empty)
			{
				Translator translator = new Translator();
				translator.Name = item.Name;
				translator.Form = sForm;
				translator.Value = item.Text;
				Translator translator2 = db.Translators().FirstOrDefault((Translator x) => x.Form == form.Name && x.Name == item.Name);
				if (translator2 != null)
				{
					translator.Value = translator2.Value;
					translator.Id = translator2.Id;
				}
				entLanguages.Add(translator);
			}
		}

		private static void Item_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			Control control = (Control)sender;
			if (control.Text != string.Empty)
			{
				Translator translator = db.Translators().FirstOrDefault((Translator x) => x.Form == sForm && x.Name == control.Name);
				if (translator != null)
				{
					string text = AniHelper.InputBoxMethod("Arabic", "Please Enter Arabic Word", "Arabic Word :", isPassword: false, control.Text);
					translator.Value = ((text == string.Empty) ? control.Text : text);
				}
				else
				{
					translator = new Translator();
					translator.Name = control.Name;
					string text2 = AniHelper.InputBoxMethod("Arabic", "Please Enter Arabic Word", "Arabic Word :", isPassword: false, control.Text);
					translator.Value = ((text2 == string.Empty) ? control.Text : text2);
					translator.Form = sForm;
				}
				Save(translator);
				control.Text = translator.Value;
			}
		}

		public static void LoadArabic(Form form)
		{
			if (!(Settings.Default.Language != "en"))
			{
				return;
			}
			List<Translator> list = new List<Translator>();
			list = (from x in db.Translators()
				where x.Form == form.Name
				select x).ToList();
			foreach (Control item in form.Controls)
			{
				if (!(item is Label) && !(item is Button) && !(item is RadioButton) && !(item is CheckBox))
				{
					if (item is Panel)
					{
						Panel panel = (Panel)item;
						foreach (Control ctrl2 in panel.Controls)
						{
							if (ctrl2 is Label || ctrl2 is Button || ctrl2 is RadioButton || ctrl2 is CheckBox)
							{
								Translator translator = list.FirstOrDefault((Translator x) => x.Name == ctrl2.Name);
								if (translator != null && translator.Value != string.Empty && translator.Value != null)
								{
									ctrl2.Text = translator.Value;
								}
							}
						}
					}
					else
					{
						if (!(item is TabControl))
						{
							continue;
						}
						TabControl tabControl = (TabControl)item;
						foreach (TabPage tabPage in tabControl.TabPages)
						{
							foreach (Control ctrl in tabPage.Controls)
							{
								if (!(ctrl is Label) && !(ctrl is Button) && !(ctrl is RadioButton) && !(ctrl is CheckBox))
								{
									if (!(ctrl is Panel))
									{
										continue;
									}
									Panel panel2 = (Panel)ctrl;
									foreach (Control y in panel2.Controls)
									{
										if (y is Label || y is Button || y is RadioButton || y is CheckBox)
										{
											Translator translator2 = list.FirstOrDefault((Translator x) => x.Name == y.Name);
											if (translator2 != null && translator2.Value != string.Empty && translator2.Value != null)
											{
												y.Text = translator2.Value;
											}
										}
									}
								}
								else
								{
									Translator translator3 = list.FirstOrDefault((Translator x) => x.Name == ctrl.Name);
									if (translator3 != null && translator3.Value != string.Empty && translator3.Value != null)
									{
										ctrl.Text = translator3.Value;
									}
								}
							}
						}
					}
				}
				else
				{
					Translator translator4 = list.FirstOrDefault((Translator x) => x.Name == item.Name);
					if (translator4 != null && translator4.Value != string.Empty && translator4.Value != null)
					{
						item.Text = translator4.Value;
					}
				}
			}
		}
	}
}
