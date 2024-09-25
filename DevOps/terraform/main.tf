data "azurerm_resource_group" "rg" {
  name = var.resource_group_name
}

# Azure Storage
resource "azurerm_storage_account" "example" {
  name                     = "${var.functionapp_storage_account_name}"
  resource_group_name      = data.azurerm_resource_group.rg.name
  location                 = var.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_application_insights" "application_insights" {
  name                = "application-insights"
  location            = var.location
  resource_group_name = data.azurerm_resource_group.rg.name
  application_type    = "web"
}

# Azure Service Plan
resource "azurerm_service_plan" "example" {
  name                = "rk-app-service-plan01"
  resource_group_name = data.azurerm_resource_group.rg.name
  location            = var.location
  os_type             = "Windows"
  # Windows Consumption
  sku_name            = "Y1"
}

# Azure Function
resource "azurerm_windows_function_app" "example" {
  name                = "${var.azurerm_windows_function_app_name}"
  resource_group_name = data.azurerm_resource_group.rg.name
  location            = var.location

  storage_account_name = azurerm_storage_account.example.name
  storage_account_access_key = azurerm_storage_account.example.primary_access_key
  service_plan_id      = azurerm_service_plan.example.id

  app_settings = {
    "FUNCTIONS_WORKER_RUNTIME" = "dotnet-isolated",
    "APPINSIGHTS_INSTRUMENTATIONKEY" = azurerm_application_insights.application_insights.instrumentation_key,
  }

  site_config {
  }
}