build_ch1:
	docker build -t sierrahackingco/grayhat_csharp_ch1:latest -f ch1/Dockerfile .

run_ch1:
	docker run -it --rm --name grayhat_csharp_ch1 sierrahackingco/grayhat_csharp_ch1:latest

deploy_ch1:
	make build_ch1
	make run_ch1

build_ch2:
	docker build -t sierrahackingco/grayhat_csharp_ch2:latest -f ch2/Dockerfile .

run_ch2:
	docker compose -f ch2/docker-compose.yml up -d --wait
	docker attach grayhat_csharp_ch2

teardown_ch2:
	docker compose -f ch2/docker-compose.yml down

deploy_ch2:
	make teardown_ch2
	make build_ch2
	make run_ch2
	
help:
	@echo "deploy_ch1 - Build and deploy Chapter 1\ndeploy_ch2 - Build and deploy Chapter 2"