terraform {
  required_version = ">=1.0" # https://github.com/hashicorp/terraform/releases

  required_providers {

    azurerm = {
      source = "hashicorp/azurerm"
      version = "3.71"
    }
  }

  # If a configuration includes no backend block, Terraform defaults to using the local backend, which stores state as a plain file in the current working directory.
  backend "azurerm" {
    version = "3.71"
  }

}

provider "azurerm" {
  features {}
  skip_provider_registration = "true"
}