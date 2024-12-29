# Using Ubuntu 24.04
# Builder Image Chapter 1
FROM ubuntu:24.04 AS csharp-builder-image-ch1

# Avoid stuck build due to user prompt
ARG DEBIAN_FRONTEND=noninteractive

RUN apt-get update && apt-get install -y tzdata software-properties-common make ca-certificates gnupg \
      && gpg --homedir /tmp --no-default-keyring --keyring /usr/share/keyrings/mono-official-archive-keyring.gpg --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF \
      && echo "deb [signed-by=/usr/share/keyrings/mono-official-archive-keyring.gpg] https://download.mono-project.com/repo/ubuntu stable-focal main" | tee /etc/apt/sources.list.d/mono-official-stable.list \
      && apt update && apt-get install --no-install-recommends -y \
      && apt-get install -y mono-devel \
      && apt-get clean \
      && rm -rf /var/lib/apt/lists/*/*

# Using final folder pathing to make copies easy
RUN mkdir /home/gundam
WORKDIR /home/gundam

# Copy in src folders and Makefile
COPY /ch1/helloWorld /home/gundam/helloWorld
COPY /ch1/theBasics /home/gundam/theBasics
COPY /ch1/pinvoke /home/gundam/pinvoke
COPY /ch1/Makefile /home/gundam/Makefile

RUN make ch1

# Runner Image
FROM ubuntu:24.04 AS runner-image

RUN apt-get update && apt-get upgrade -y \
      && apt-get install -y tzdata software-properties-common ca-certificates gnupg \
      && gpg --homedir /tmp --no-default-keyring --keyring /usr/share/keyrings/mono-official-archive-keyring.gpg --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF \
      && echo "deb [signed-by=/usr/share/keyrings/mono-official-archive-keyring.gpg] https://download.mono-project.com/repo/ubuntu stable-focal main" | tee /etc/apt/sources.list.d/mono-official-stable.list \
      && apt-get install --no-install-recommends -y \
      && apt-get install -y mono-complete \
      && apt-get clean \
      && rm -rf /var/lib/apt/lists/*

# Create gundam bot user and copy over compiled exe files
RUN useradd --create-home gundam
COPY --from=csharp-builder-image-ch1 --chown=gundam:gundam /home/gundam/ch1_helloWorld.exe /home/gundam/ch1_helloWorld.exe
COPY --from=csharp-builder-image-ch1 --chown=gundam:gundam /home/gundam/ch1_the_basics.exe /home/gundam/ch1_the_basics.exe
COPY --from=csharp-builder-image-ch1 --chown=gundam:gundam /home/gundam/ch1_pinvoke.exe /home/gundam/ch1_pinvoke.exe

# Switch from root to gundam, change to `/home/gundam` dir
USER gundam
WORKDIR /home/gundam