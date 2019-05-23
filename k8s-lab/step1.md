The Kubernetes environment is a single node Kubernetes cluster with the Master initialise using Kubeadm.

With the Kubernetes environment configured, users can focus on learning new skills on top of the platform without having to complete the steps to configure the cluster.

To provide users with feedback about when Kubernetes has been fully initialised, run the command `launch.sh`{{execute}}. This indicates to users that something is happening and when it's safe to proceed.

# k8s-lab

Go to https://www.katacoda.com/courses/kubernetes/playground

## Prepare env

`launch.sh`{{execute}}

`git clone https://github.com/mwpro/k8s-lab.git && cd k8s-lab`{{execute}}

`kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/master/deploy/mandatory.yaml -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/master/deploy/provider/baremetal/service-nodeport.yaml`{{execute}}

## Play around a little

`kubectl`{{execute}}

`kubectl cluster-info`{{execute}}

`kubectl get nodes`{{execute}}

`kubectl describe node master`{{execute}}

`kubectl get pods`{{execute}}

`kubectl get namespaces`{{execute}}

`kubectl get pods -n kube-system`{{execute}}

`kubectl get pods --all-namespaces`{{execute}}

## Create first pod

https://github.com/mwpro/k8s-lab/tree/master/Lab.Api

https://github.com/mwpro/k8s-lab/blob/master/01.pod.yaml

`kubectl apply -f 01.pod.yaml`{{execute}}

`kubectl get pods`{{execute}}

`kubectl describe pod lab-api`{{execute}}

`ip=$(kubectl describe pod lab-api | grep IP | awk '{print $2}')`{{execute}}

`curl $ip`{{execute}}

## Assign vars to pod

https://github.com/mwpro/k8s-lab/blob/master/02.pod-env.yaml#L11-L13

`kubectl apply -f 02.pod-env.yaml`{{execute}}

`kubectl delete pod lab-api`{{execute}}

`kubectl apply -f 02.pod-env.yaml`{{execute}}

`ip=$(kubectl describe pod lab-api | grep IP | awk '{print $2}')`{{execute}}

`curl $ip`{{execute}}

## First deployment

https://github.com/mwpro/k8s-lab/blob/master/03.deployment.yaml

`kubectl delete -f 02.pod-env.yaml`{{execute}}

`kubectl apply -f 03.deployment.yaml`{{execute}}

`kubectl get deployment`{{execute}}

`kubectl describe deployment lab-api`{{execute}}

`kubectl get pods`{{execute}}

`kubectl describe pod lab-api | grep IP`{{execute}}

`curl xxx`

`kubectl scale deployment lab-api --replicas=10 && watch -n 1 kubectl get pods`{{execute}}

`kubectl edit deployment lab-api && watch -n 1 kubectl get pods`{{execute}}

`kubectl scale deployment lab-api --replicas=3 && watch -n 1 kubectl get pods`{{execute}}

## Service

https://github.com/mwpro/k8s-lab/blob/master/04.service.yaml

`kubectl apply -f 04.service.yaml`{{execute}}

`kubectl get svc`{{execute}}

`kubectl describe service lab-api`{{execute}}

`ip=$(kubectl describe service lab-api | grep IP: | awk '{print $2}')`{{execute}}

`watch -n 1 curl $ip`{{execute}}

`kubectl edit deployment lab-api && watch -n 1 curl $ip`{{execute}}

`status.podIP`

## Ingress

https://github.com/mwpro/k8s-lab/blob/master/05.ingress.yaml

`kubectl apply -f 05.ingress.yaml`{{execute}}

`kubectl get ingress`{{execute}}

`kubectl describe ingress lab-ingress`{{execute}}

`ingressIp=$(kubectl describe service ingress-nginx -n ingress-nginx | grep IP: | awk '{print $2}')`{{execute}}

`curl $ip`{{execute}}

`curl --resolve lab.foo:80:$ingressIp lab.foo`{{execute}}

## Frontend deployment

https://github.com/mwpro/k8s-lab/blob/master/06.deployment-2.yaml

`kubectl apply -f 06.deployment-2.yaml`{{execute}}

`kubectl get svc`{{execute}}

`ip=$(kubectl describe service lab-frontend | grep IP: | awk '{print $2}')`{{execute}}

`curl $ip`{{execute}}

## Advanced ingress rules

https://github.com/mwpro/k8s-lab/blob/master/07.ingress-routing.yaml#L12-L19

`kubectl apply -f 07.ingress-routing.yaml`{{execute}}

`curl --resolve lab.foo:80:$ingressIp lab.foo`{{execute}}

`curl --resolve lab.foo:80:$ingressIp lab.foo/api`{{execute}}