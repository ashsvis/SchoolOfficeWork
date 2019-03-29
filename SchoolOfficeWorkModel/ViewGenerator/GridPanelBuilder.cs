using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ViewGenerator
{
    public static class GridPanelBuilder
    {
        public static event Action<string, string> Error = delegate { };

        private static void OnError(string message, string caption)
        {
            Error(message, caption);
        }

        public static UserControl BuildPropertyPanel(object userModel, object userClass, object userCollection)
        {
            // заготовка для таблицы записей
            var userControl = new UserControl
            {
                Dock = DockStyle.Fill
            };
            // создаём сетку для компоновки
            var grid = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2
            };
            grid.ColumnStyles.Add(new ColumnStyle());
            grid.RowStyles.Add(new RowStyle());
            grid.RowStyles.Add(new RowStyle() { SizeType = SizeType.Percent, Height = 100 });
            // кнопка "Добавить"
            var btnAdd = new Button
            {
                Text = "Добавить",
                Anchor = AnchorStyles.Left,
                AutoSize = true
            };
            // заготовка для таблицы
            var listView = new ListView
            {
                Dock = DockStyle.Fill,
                MultiSelect = false,
                FullRowSelect = true,
                HideSelection = false,
                ShowItemToolTips = true,
                VirtualMode = true,
                View = View.Details
            };
            var btnEdit = new Button
            {
                Text = "Изменить",
                Anchor = AnchorStyles.Left,
                AutoSize = true,
                Enabled = false
            };
            var btnDelete = new Button
            {
                Text = "Удалить",
                Anchor = AnchorStyles.Left,
                AutoSize = true,
                Enabled = false
            };
            // обработчик действия при нажатии кнопки "Добавить"
            btnAdd.Click += (o, e) =>
            {
                // создаём пустой объект требуемого типа
                var item = Activator.CreateInstance(userClass.GetType());
                // вызываем диалог для заполнения свойств объекта
                var frm = PropertyPanelBuilder.ShowPropertyFormDialog(userModel, item);
                // если не была нажата клавиша "Ввод", выходим
                if (frm.DialogResult != DialogResult.OK) return;
                // формируем данные для вызова метода Add коллекции объектов:
                // заказываем типы параметров для вызова метода
                Type[] parameterTypes = { item.GetType() };
                // создаём ссылку на метод Add, с формальным списком параметров 
                MethodInfo method = userCollection.GetType().GetMethod("Add", parameterTypes);
                // формируем массив значений параметров для передачи при вызове метода
                object[] arguments = { item };
                try
                {
                    // вызываем метод на коллекции объектов с аргументами
                    method.Invoke(userCollection, arguments);
                    listView.VirtualListSize = 0; // сбросим виртуальный размер
                    listView.VirtualListSize = ((IEnumerable<object>)userCollection).Count(); // установим размер по размеру коллекции
                    listView.Invalidate(); // просим обновить вид
                    btnEdit.Enabled = btnDelete.Enabled = false;
                    // ищем новую строку в списке
                    var lvi = listView.FindItemWithText(item.ToString());
                    if (lvi != null) // если найдена, то делаем ее текущей
                    {
                        lvi.Selected = true;
                        lvi.EnsureVisible();
                    }
                }
                catch (Exception ex)
                {
                    OnError(ex.InnerException.Message, "Добавление записи");
                }
            };
            btnEdit.Click += (o, e) =>
            {
                if (listView.SelectedIndices.Count == 0) return;
                // получаем ссылку на коллекцию
                var collection = (IEnumerable<object>)userCollection;
                // получаем ссылку на редактируемый элемент
                var item = collection.ElementAt(listView.SelectedIndices[0]);
                // вызываем диалог для заполнения свойств объекта
                var frm = PropertyPanelBuilder.ShowPropertyFormDialog(userModel, item);
                // если не была нажата клавиша "Ввод", выходим
                if (frm.DialogResult != DialogResult.OK) return;
                listView.Invalidate(); // просим обновить вид
            };
            btnDelete.Click += (o, e) =>
            {
                if (listView.SelectedIndices.Count == 0) return;
                if (MessageBox.Show("Удалить текущую строку?", "Удаление строки",
                                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                                    MessageBoxDefaultButton.Button2) != DialogResult.Yes) return;
                // получаем ссылку на коллекцию
                var collection = (IEnumerable<object>)userCollection;
                // получаем ссылку на редактируемый элемент
                var item = collection.ElementAt(listView.SelectedIndices[0]);
                // формируем данные для вызова метода Add коллекции объектов:
                // заказываем типы параметров для вызова метода
                Type[] parameterTypes = { item.GetType() };
                // создаём ссылку на метод Remove, с формальным списком параметров 
                MethodInfo method = userCollection.GetType().GetMethod("Remove", parameterTypes);
                // формируем массив значений параметров для передачи при вызове метода
                object[] arguments = { item };
                // вызываем метод на коллекции объектов с аргументами
                method.Invoke(userCollection, arguments);
                listView.VirtualListSize = 0; // сбросим виртуальный размер
                listView.VirtualListSize = ((IEnumerable<object>)userCollection).Count(); // установим размер по размеру коллекции
                listView.Invalidate(); // просим обновить вид
                btnEdit.Enabled = btnDelete.Enabled = false;
            };
            // создадим панель для размещения кнопок
            var flow = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Left
            };
            // добавим кнопки в панель кнопок
            flow.Controls.Add(btnAdd);
            flow.Controls.Add(btnEdit);
            flow.Controls.Add(btnDelete);
            // добавим панель кнопок в сетку
            grid.Controls.Add(flow, 0, 0);
            // получаем тип объекта, переданного через параметр
            var type = userClass.GetType();
            MemberInfo[] m = type.GetProperties();  // получаем массив свойств объекта
            var count = 0;
            foreach (var info in m) // для каждого свойства из массива свойств
            {
                // получаем ссылку на свойство по его имени
                var prop = type.GetProperty(info.Name);
                // получаем наименование свойства из дескриптора
                var caption = PropertyPanelBuilder.GetPropertyCaption(userClass, prop);
                var typeName = prop.PropertyType.ToString();
                var width = 120;
                var textAlign = HorizontalAlignment.Left;
                switch (typeName)
                {
                    case "System.Guid":
                        if (count++ == 0) continue;
                        break;
                    case "System.String":
                        if (PropertyPanelBuilder.CheckPasswordMode(userClass, prop)) continue;
                        width = 120;
                        break;
                    case "System.Int32":    // для целочисленных свойств
                        textAlign = HorizontalAlignment.Right;
                        width = 70;
                        break;
                    case "System.Decimal":  // для свойств с ценой
                        textAlign = HorizontalAlignment.Right;
                        width = 70;
                        break;
                    case "System.DateTime": // для свойств с датой
                        textAlign = HorizontalAlignment.Center;
                        width = 90;
                        break;
                    case "System.Boolean": // для логических свойств
                        textAlign = HorizontalAlignment.Center;
                        width = 60;
                        break;
                    default:
                        continue;
                }
                listView.Columns.Add(new ColumnHeader
                {
                    Text = string.IsNullOrWhiteSpace(caption) ? prop.Name : caption,
                    TextAlign = textAlign,
                    Width = width
                });
            }
            // цепляем обработчик для виртуального режима
            listView.RetrieveVirtualItem += (o, e) =>
            {
                e.Item = new ListViewItem();
                // получаем ссылку на коллекцию
                var collection = (IEnumerable<object>)userCollection;
                // получаем ссылку на рисуемый элемент
                var item = collection.ElementAt(e.ItemIndex);
                // получаем его тип
                type = item.GetType();
                m = type.GetProperties();  // получаем массив свойств объекта
                count = 0; // счетчик столбцов
                for (var i = 0; i < m.Length; i++)      // для каждого свойства из массива свойств
                {
                    // получаем ссылку на свойство по его имени
                    var prop = type.GetProperty(m[i].Name);
                    // если свойство первое и его тип Guid (ключевой столбец) - пропускаем
                    if (i == 0 && prop.PropertyType == typeof(Guid)) continue;
                    string value; // здесь будет значение
                    if (prop.PropertyType == typeof(DateTime))
                        value = ((DateTime)prop.GetValue(item)).ToShortDateString();
                    else if (prop.PropertyType == typeof(bool))
                        value = ((bool)prop.GetValue(item)) ? "Да" : "Нет";
                    else if (prop.PropertyType == typeof(decimal))
                        value = ((decimal)prop.GetValue(item)).ToString("0.00");
                    else if (prop.PropertyType == typeof(int))
                        value = ((int)prop.GetValue(item)).ToString("0");
                    else if (prop.PropertyType == typeof(string))
                    {
                        if (PropertyPanelBuilder.CheckPasswordMode(userClass, prop)) continue;
                        value = prop.GetValue(item)?.ToString();
                    }
                    else if (prop.PropertyType == typeof(Guid))
                        value = GetLookupName(prop, item, userModel);
                    else
                        continue;
                    // если небыло столбцов, пишем в первый
                    if (count++ == 0)
                        e.Item.Text = value;
                    else // иначе добавляем
                        e.Item.SubItems.Add(value);
                }
            };
            // цепляем обработчик для виртуального поиска
            listView.SearchForVirtualItem += (o, e) => 
            {
                // e.Text содержит строковое представление объекта (перегрузкой метода ToString())
                // получаем ссылку на коллекцию
                var collection = (IEnumerable<object>)userCollection;
                var n = 0;
                foreach (var item in collection)
                {
                    if (item.ToString() == e.Text)
                    {
                        e.Index = n;
                        break;
                    }
                    n++;
                }
            };
            // указываем размер коллекции
            listView.VirtualListSize = ((IEnumerable<object>)userCollection).Count();
            // разрешаем кнопки редактирования
            listView.SelectedIndexChanged += (o, e) => 
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            };

            // добавим детальный список в сетку
            grid.Controls.Add(listView, 0, 1);
            // добавим сетку на панель свойств
            userControl.Controls.Add(grid);
            // возвращаем сформированный контрол
            return userControl;
        }

        /// <summary>
        /// Извлечение текстового представления связанного ключевого поля
        /// </summary>
        /// <param name="prop">Guid - свойство для подключения связанного справочника</param>
        /// <param name="item">текущий объект строки</param>
        /// <param name="userModel">объект корневого объекта модели</param>
        /// <returns>Найденное строковое представление</returns>
        public static string GetLookupName(PropertyInfo prop, object item, object userModel)
        {
            if (prop.PropertyType != typeof(Guid)) return "(error: guid prop expecteded)";
            var value = (Guid)prop.GetValue(item);
            // получаем ссылку на коллекцию атрибутов свойства
            var attributes = TypeDescriptor.GetProperties(item)[prop.Name].Attributes;
            // получаем из коллекции атрибутов ссылку на атрибут источника данных свойства
            var mAttribute = (DataLookupAttribute)attributes[typeof(DataLookupAttribute)];
            // возвращаем текстовое представления ключа, если атрибут для поиска не определён
            if (mAttribute == null) return value.ToString();
            var valueMember = mAttribute.ValueMember;    // имя ключевого свойства в коллекции
            var lookupMember = mAttribute.LookupMember;  // имя перечисления коллекции объектов ("ключевой" список)
            // ищем соответствие ключу в "ключевом" списке, заданном в атрибуте DataLookupAttribute
            foreach (var lookup in (IEnumerable<object>)userModel.GetType()
                                   .GetProperty(lookupMember).GetValue(userModel))
            {
                // получаем свойство, заданное в аттрибуте DataLookupAttribute, объекта ключевой коллеции
                var lookprop = lookup.GetType().GetProperty(valueMember);
                // работаем только с типом Guid
                if (lookprop.PropertyType != typeof(Guid)) continue;
                // получаем значение ключа
                var itemValue = (Guid)lookprop.GetValue(lookup);
                if (itemValue == value) // если ключи совпадают, то возвращаем заданнае текстовое представление
                    return lookup.ToString();
            }
            // иначе возвращаем строковое представление ключа
            return value.ToString();
        }
    }
}
