build_ch1:
	docker build -t sierrahackingco/grayhat_csharp_ch1:latest -f ch1/Dockerfile .

run_ch1:
	docker run -it --rm --name grayhat_csharp_ch1 sierrahackingco/grayhat_csharp_ch1:latest

test_ch1:
	make build_ch1
	make run_ch1

build_ch2:
	docker build -t sierrahackingco/grayhat_csharp_ch2:latest -f ch2/Dockerfile .

run_ch2:
	docker run -it --rm --name grayhat_csharp_ch2 sierrahackingco/grayhat_csharp_ch2:latest

test_ch2:
	make build_ch2
	make run_ch2

help:
	@echo " test_ch1 - Build and deploy Chapter 1\n test_ch2 - Build and deploy Chapter 2"