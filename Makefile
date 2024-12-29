build_ch1:
	docker build -t sierrahackingco/grayhat_csharp_ch1:latest -f ch1/Dockerfile .

test_ch1:
	docker run -it --name grayhat_csharp_ch1 sierrahackingco/grayhat_csharp_ch1:latest

help:
	@echo "build_ch1 test_ch1"