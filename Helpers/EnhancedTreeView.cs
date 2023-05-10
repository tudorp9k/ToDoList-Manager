using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace ToDoList.Helpers
{
    /// <summary>
    /// This treeview class allows databinding the
    /// "SelectedObject" property
    /// </summary>
    public class EnhancedTreeView : TreeView
    {
        #region SelectedObject

        /// <summary>
        /// The dependency property that allows use to bind the
        /// "SelectedObject" property
        /// </summary>
        public static readonly DependencyProperty
          SelectedObjectProperty =
            DependencyProperty.Register(
              "SelectedObject",
              typeof(object),
              typeof(EnhancedTreeView),
              new PropertyMetadata(SelectedObjectChangedCallback));

        /// <summary>
        /// Gets or sets the select object (a writable version of
        /// the "SelectedItem" property)
        /// </summary>
        [Bindable(true)]
        public object SelectedObject
        {
            get { return GetValue(SelectedObjectProperty); }
            set { SetValue(SelectedObjectProperty, value); }
        }

        /// <summary>
        /// This method is called whenever ever the selected
        /// object is changed, and if it was changed from the
        /// outside, this method will set the selected item.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="eventArgs"></param>
        private static void SelectedObjectChangedCallback
          (DependencyObject obj,
           DependencyPropertyChangedEventArgs eventArgs)
        {
            EnhancedTreeView treeView = (EnhancedTreeView)obj;
            if (!ReferenceEquals(treeView.SelectedItem,
                  eventArgs.NewValue))
                SelectItem(treeView, eventArgs.NewValue);
        }

        #endregion

        /// <summary>
        /// Searches the given item in the parent (recursively)
        /// and selects it, returns true if the item was found
        /// and selected, false otherwise.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="itemToSelect"></param>
        /// <returns></returns>
        private static bool SelectItem
          (ItemsControl parent, object itemToSelect)
        {
            var childTreeNode =
              parent.ItemContainerGenerator
                .ContainerFromItem(itemToSelect)
              as TreeViewItem;

            // if the item to select is directly under "parent",
            // just select it
            if (childTreeNode != null)
            {
                childTreeNode.Focus();
                return childTreeNode.IsSelected = true;
            }

            // if the item to select is not directly under "parent",
            // search the child nodes of "parent"
            if (parent.Items.Count > 0)
            {
                foreach (object childItem in parent.Items)
                {
                    var childItemsControl =
                      parent.ItemContainerGenerator
                        .ContainerFromItem(childItem)
                      as ItemsControl;

                    if (SelectItem(childItemsControl, itemToSelect))
                        return true;
                }
            }

            // if the given item wasn't found here:
            return false;
        }

        /// <summary>
        /// When the selected item is updated from inside the tree,
        /// this method will update the "SelectedObject" property.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSelectedItemChanged
          (RoutedPropertyChangedEventArgs<object> e)
        {
            this.SelectedObject = e.NewValue;

            base.OnSelectedItemChanged(e);
        }
    }
}
