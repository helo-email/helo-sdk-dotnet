#!/usr/bin/env bash
set -eo pipefail

if [ -z "$1" ]
then
  echo "Package version must be specified"
  exit
fi

if [ -z "$helo_pkg_token" ]
then
  echo "Github access token must be set in environment variable 'helo_pkg_token'"
  exit
fi

version=$1
source="https://nuget.pkg.github.com/helo-email/index.json"
package="bin/Release/HeloEmail.Sdk.$version.nupkg"

cd src/HeloEmail.Sdk

dotnet pack --configuration Release -p:PackageVersion=$version

dotnet nuget push $package \
  --api-key $helo_pkg_token \
  --source $source \
  --skip-duplicate