variable "environment" {
  type = string
}

variable "resource_group_name" {
  type = string
}

variable "location" {
  type = string
}

variable "functionapp_storage_account_name" {
  type = string
  default = ""
}

variable "azurerm_windows_function_app_name" {
  type = string
  default = ""
}
