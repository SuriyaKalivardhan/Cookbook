from typing import TypedDict, Union, List
from langchain_core.messages import HumanMessage, AIMessage
from langchain_openai import ChatOpenAI
from langgraph.graph import StateGraph, START, END

from dotenv import load_dotenv

load_dotenv()
llm = ChatOpenAI(model='gpt-4o')

class AgentState(TypedDict):
    messages: List[Union[HumanMessage, AIMessage]]

def processor(state:AgentState) -> AgentState:
    '''Processor the invput message with conversation memory'''
    response = llm.invoke(state['messages'])
    print(f"AI: {response.content}")
    state['messages'].append(response.content)

graph = StateGraph(AgentState)
graph.add_node('process_node', processor)
graph.add_edge(START, 'process_node')
graph.add_edge('process_node', END)
app = graph.compile()

conversation_history = []
user_input = input("User:")
while user_input != "exit":
    conversation_history.append(HumanMessage(content=user_input))
    result = app.invoke({"messages":conversation_history})
    conversation_history = result['messages']
    user_input = input("User:")