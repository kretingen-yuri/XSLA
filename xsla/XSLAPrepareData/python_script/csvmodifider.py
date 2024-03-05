import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
import seaborn as sns
from matplotlib import pylab
from itertools import combinations
import nltk
from nltk.tokenize import word_tokenize
from nltk.stem import PorterStemmer, WordNetLemmatizer
import sys

print(sys.argv)

def sign(num):
    if num > 0:
        return 1
    elif num < 0:
        return -1
    else:
        return 0

params = {'xtick.labelsize': 18,
 'ytick.labelsize': 18,
 'axes.titlesize' : 22,
 'axes.labelsize' : 20,
 'legend.fontsize': 18,
 'legend.title_fontsize': 22,
 'figure.titlesize': 24 }
pylab.rcParams.update(params)


dataset = pd.read_csv(sys.argv[1])
num_of_rows=dataset.shape[0]




# Initialize stemmer and lemmatizer
stemmer = PorterStemmer()
lemmatizer = WordNetLemmatizer()

# Define the list of features and their variants
featuresTitleandDescribtion = [
    ["driver", "Driver"],
    ["vra", "Vras", "VRA"],
    ["ghost vra", "Ghost vra", "ghost_vra", "ghost VRA"],
    ["vpg", "VPGs", "Vpg"],
    ["fol", "FOTs", "FOLs"],
    ["move", "MOVEs"],
    ["ssh", "Sshs", "SSHs"],
    ["install", "installing", "Install", "Installation", " install", "installs"],
    ["host logs", "host logs", "Host logs"],
    ["upgrade", "Upgrade", "upgrading", "Upgraded", "Upon upgrading", "After upgrade", "after completing the upgrade", "After upgrading"],
    ["crash", "crashes", "Crash", "crashing"],
    ["WA", "WA", "WORKAROUND", "workaround", "work around"],
    ["fixed", "Fixed", "Fixes"],
    ["fully", "Fully"],
    ["env is ok", "env is ok"],
    ["msp", "csp", "end customer", "cloud provider"],
    ["vcd"],
    ["keycloak", "kick lock", "kick-lock"],
    ["ltr", "ejc"],
    ["network"],
    ["Firewall", "fw"],
    ["proxy"]
]

# Define a dictionary to store the stemmed and lemmatized variants
stemmed_variants_dict = {}
lemmatized_variants_dict = {}

# Define a function to get stemmed and lemmatized variants for a token
def get_variants (token):
    stemmed_token = stemmer.stem(token)
    lemmatized_token = lemmatizer.lemmatize(token)
    if stemmed_token not in stemmed_variants_dict:
        stemmed_variants_dict[stemmed_token] = stemmed_token
    if lemmatized_token not in lemmatized_variants_dict:
        lemmatized_variants_dict[lemmatized_token] = lemmatized_token
    return stemmed_variants_dict[stemmed_token], lemmatized_variants_dict[lemmatized_token]

# Define a function to check if tokens contain stemmed or lemmatized variants of features
def checkWords(long_text, featuresTitleandDescribtion):
    tokens = word_tokenize(long_text)
    final = []
    for feature in featuresTitleandDescribtion:
        variants = feature[0]
        stemmed_variants, lemmatized_variants = get_variants(variants)
        found = False
        for token in tokens:
            stemmed_token, lemmatized_token = get_variants (token)
            if stemmed_token in stemmed_variants or lemmatized_token in lemmatized_variants:
                final.append(1)
                dataset[variants] = 1
                found = True
                break
        if not found:
            final.append(0)
            dataset[variants] = 0
    return final
finaaa = dataset['Description'].apply(lambda x: checkWords(x, featuresTitleandDescribtion))


#Priority
dataset['Priority'] = np.where(dataset['Priority'].astype(str).str.contains("P0"), 0, dataset['Priority']) 
dataset['Priority'] = np.where(dataset['Priority'].astype(str).str.contains("P1"), 1, dataset['Priority']) 
dataset['Priority'] = np.where(dataset['Priority'].astype(str).str.contains("P2"), 2, dataset['Priority']) 
dataset['Priority'] = np.where(dataset['Priority'].astype(str).str.contains("P3"), 3, dataset['Priority']) 
dataset['Priority'] = np.where(dataset['Priority'].astype(str).str.contains("P4"), 4, dataset['Priority']) 
dataset['Priority'] = np.where(dataset['Priority'].astype(str).str.contains("P5"), 5, dataset['Priority']) 
dataset['Priority']=dataset['Priority'].astype(int)

#ProblematicPlatform
dataset['ProblematicPlatform']=0
dataset['ProblematicPlatform'] = np.where((dataset['ToPlatform'].astype(str).str.contains('AWS', case=False) |
                                           dataset['ToPlatform'].astype(str).str.contains('Azure', case=False) |
                                           dataset['FromPlatform'].astype(str).str.contains('AWS', case=False) |
                                           dataset['FromPlatform'].astype(str).str.contains('Azure', case=False) ) , 1, 0) 
#CustomerName
dataset['CustomerName_int']=0
dataset['CustomerName_int'] = dataset['CustomerName'].factorize()[0]

#AffectedVersion
dataset['AffectedVersion'] = dataset['AffectedVersion'].astype('object')
dataset['AffectedVersion_int'] = dataset['AffectedVersion'].factorize()[0]

#IsEscalationTeam
dataset['IsEscalationTeam'] = np.where(dataset['IsEscalationTeam'].astype(bool)==True, 1, 0) 

#Initiative
dataset['Initiative_int']=0
dataset['Initiative_int'] = dataset['Initiative'].factorize()[0]

#ZVMZCAOsType
dataset['ZVMZCAOsType_int']=0
dataset['ZVMZCAOsType_int'] = dataset['ZVMZCAOsType'].factorize()[0]

#Component
dataset['Component_int']=0
dataset['Component_int'] = dataset['Component'].factorize()[0]


if sys.argv[3]=="true":
    #pairplots
    sns.set_style("whitegrid")
    mean_working_days = dataset.groupby('IsEscalationTeam')['NumberOfWorkingDays'].mean()
    sns.scatterplot(x='IsEscalationTeam', y='NumberOfWorkingDays', data=dataset, hue='IsEscalationTeam', marker='o', s=100)
    for team, mean_days in mean_working_days.items():
        plt.axhline(y=mean_days, color='r', linestyle='--', label=f'Mean for {team}: {mean_days:.2f}')
    plt.xlabel('IsEscalationTeam')
    plt.ylabel('NumberOfWorkingDays')
    plt.title('Scatter Plot with Mean Number of Working Days')
    plt.legend()
    plt.show()

    #pairplots
    sns.set_style("whitegrid")
    sns.scatterplot(x=dataset['CreatedMonth'], y=dataset['NumberOfWorkingDays'], marker='o', s=100)
    plt.xlabel('CreatedMonth')
    plt.ylabel('NumberOfWorkingDays')
    plt.title('Scatter Plot')
    plt.show()





    sns.set_style("whitegrid")
    sns.scatterplot(x=dataset['ZVMZCAOsType_int'], y=dataset['NumberOfWorkingDays'], marker='o', s=100)
    plt.xlabel('ZVMZCAOsType_int')
    plt.ylabel('NumberOfWorkingDays')
    plt.title('Scatter Plot')
    plt.show()

    sns.set_style("whitegrid")
    sns.scatterplot(x=dataset['CustomerName_int'], y=dataset['NumberOfWorkingDays'], marker='o', s=100)
    plt.xlabel('CustomerName_int')
    plt.ylabel('NumberOfWorkingDays')
    plt.title('Scatter Plot')
    plt.show()

    plt.xticks(rotation=45, fontsize=8) 
    sns.set_style("whitegrid")
    sns.set(font_scale=0.5)
    sns.scatterplot(x=dataset['AffectedVersion'], y=dataset['NumberOfWorkingDays'])
    plt.xlabel('AffectedVersion')
    plt.ylabel('NumberOfWorkingDays')
    plt.title('Scatter Plot')
    plt.show()



    #correlation
    target_column = 'NumberOfWorkingDays'
    for column in dataset.columns:
        if column != target_column and dataset[column].dtype != 'object' :
            correlation = dataset[column].corr(dataset[target_column])
            print("Correlation between {} and {} is: {:.3f}".format(column, target_column, correlation))




# save dataset
print("-----------------------------------------")
print(dataset.dtypes)






dataset = dataset.drop('Description', axis=1)
dataset.to_csv(sys.argv[2])





