# Using Ubuntu 24.04
# Builder Image
FROM Ubuntu:24.04 as csharp-builder-image

# Avoid stuck build due to user prompt
ARG DEBIAN_FRONTEND=noninteractive

RUN apt-get update && apt-get install -y tzdata software-properties-common ca-certificates gnupg \
      && sudo gpg --homedir /tmp --no-default-keyring --keyring /usr/share/keyrings/mono-official-archive-keyring.gpg --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF \
      && echo "deb [signed-by=/usr/share/keyrings/mono-official-archive-keyring.gpg] https://download.mono-project.com/repo/ubuntu stable-focal main" | sudo tee /etc/apt/sources.list.d/mono-official-stable.list \
      && sudo apt update && apt-get install --no-install-recommends -y \
      && apt-get install mono-devel \
      && apt-get clean \
      && rm -rf /var/lib/apt/lists/*/*

# Function to see all dir names and then `mcs <all .cs files in dir> -out:<dir_name.exe>